//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Projet_login_1
{
    using System;
    using System.Collections.Generic;
    
    public partial class materiel_difectue
    {
        public int id_materiel { get; set; }
        public string description_MD { get; set; }
    
        public virtual detail_materiel detail_materiel { get; set; }
    }
}
