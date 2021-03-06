﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain.Residence
{
    public class Examination
    {
        public Guid ExaminationId { get; set; }
        public Guid HospitalizationId { get; set; }
        public DateTime ExaminationStartTime { get; set; }
        public DateTime ExaminationEndTime { get; set; }
        public string Examinator { get; set; }
        public string UsedDevices { get; set; }
        public bool SignedReceip { get; set; }
        public bool SickLeave { get; set; }
        public DateTime? SickLeaveFrom { get; set; }
        public DateTime? SickLeaveTo { get; set; }
        public bool PrivateVisit { get; set; }
        public string ExaminationDetails { get; set; }
        public virtual Hospitalization Hospitalization { get; set; }
    }

    [DataContract]
    public class ExaminationTransferObject
    {
        [DataMember]
        public Guid ExaminationId { get; set; }

        [DataMember]
        public DateTime ExaminationStartTime { get; set; }

        [DataMember]
        public DateTime ExaminationEndTime { get; set; }

        [DataMember]
        public string Examinator { get; set; }

        [DataMember]
        public string UsedDevices { get; set; }

        [DataMember]
        public bool SignedReceip { get; set; }

        [DataMember]
        public bool SickLeave { get; set; }

        [DataMember]
        public DateTime? SickLeaveFrom { get; set; }

        [DataMember]
        public DateTime? SickLeaveTo { get; set; }

        [DataMember]
        public bool PrivateVisit { get; set; }

        [DataMember]
        public string ExaminationDetails { get; set; }
    }
}