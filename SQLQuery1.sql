create DATABASE SERVICES_RESSOURCES_FINANCIERES

USE SERVICES_RESSOURCES_FINANCIERES
 

create table type_service(
id_service int identity primary key,
nom_type varchar(100)
)

create table bureau(
id_bureau varchar(20) primary key,
nom_bureau varchar(100) unique,
id_service int foreign key references type_service(id_service)
)

create table type_materiel(
id_type_materiel int identity primary key,
libelle varchar(100) unique
)

create table detail_materiel(
id_materiel int identity primary key,
id_type_materiel int foreign key references type_materiel(id_type_materiel),
fiche_technique varchar(max),
etat varchar(20) check (etat='bon' or etat='moyen' or etat='difectue'),

)

create table materiel(
id_bureau varchar(20) foreign key references bureau(id_bureau),
id_type_materiel int foreign key references type_materiel(id_type_materiel),
id_materiel int foreign key references detail_materiel(id_materiel), 
primary key (id_type_materiel,id_materiel)
)
go
create or alter trigger ajoute_histoir on materiel  FOR INSERT
AS
begin

declare  @id_bureau varchar(50),@id_materiel int,@date datetime
set @date =getdate()
set @id_materiel =(select id_materiel from inserted)
set @id_bureau=(select id_bureau from inserted)
commit
insert into histoir values(@id_bureau,@id_materiel,@date)

end
drop trigger ajoute_histoir
create table histoir(
 id_histoir int identity primary key,
 id_bureau varchar(20) foreign key references bureau(id_bureau),
 id_materiel int foreign key references detail_materiel(id_materiel), 
 date_histoir datetime
)
--creation des tables 
create table materiel_difectue(
id_materiel int foreign key references detail_materiel(id_materiel),
description_MD varchar(100),/*description_materiel_difectue*/
primary key(id_materiel)
)
/* Trigger qui empeches l'ajoute d'un etat hors de trois etat disponibles  */
go
create or alter trigger verification on materiel_difectue  instead of insert 
as 
begin
declare @id int,@desc nvarchar(max);
set @id=(select id_materiel from inserted)
set @desc=(select description_MD from inserted)
if @id not in (select id_materiel from detail_materiel where etat='difectue')
rollback
else
insert into materiel_difectue values(@id,@desc)
end

go 
create or alter trigger verification_sup_mat on detail_materiel  instead of delete
as
begin 
declare  @id int =( select id_materiel from deleted)
delete from materiel_difectue where id_materiel=@id;
delete from materiel where id_materiel=@id;
delete from detail_materiel where id_materiel=@id;


end


go
create or alter proc rechrech_detail(@id int) as
begin
declare @et varchar(50)
set @et=(select etat from detail_materiel where id_materiel=@id)
if @et='difectue'
begin
select tm.libelle,dt.*,mt.id_bureau,mdf.description_MD from detail_materiel dt join materiel mt on dt.id_materiel=mt.id_materiel join materiel_difectue mdf on mdf.id_materiel=dt.id_materiel
join type_materiel tm on tm.id_type_materiel=dt.id_type_materiel where dt.id_materiel=@id
end
else if @et!='difectue'
begin
select tm.libelle,dt.*,mt.id_bureau from detail_materiel dt join materiel mt on dt.id_materiel=mt.id_materiel 
join type_materiel tm on tm.id_type_materiel=dt.id_type_materiel where dt.id_materiel=1

end
end
exec rechrech_detail id_materiel 