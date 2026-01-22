namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;
    using System;

    /// <summary>
    /// PowerTransformerConverter has methods for populating
    /// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
    /// </summary>
    public static class PowerTransformerConverter
	{

		#region Populate ResourceDescription
		public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
		{
			if ((cimIdentifiedObject != null) && (rd != null))
			{
				if (cimIdentifiedObject.NameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
				}
				if (cimIdentifiedObject.AliasNameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_ALIASNAME, cimIdentifiedObject.AliasName));
				}
				if (cimIdentifiedObject.MRIDHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
				}
			}
		}

        public static void PopulateTerminalProperties(FTN.Terminal cimTerminal, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimTerminal != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimTerminal, rd);

                if (cimTerminal.ConductingEquipmentHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimTerminal.ConductingEquipment.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimTerminal.GetType().ToString()).Append(" rdfID = \"").Append(cimTerminal.ID);
                        report.Report.Append("\" - Failed to set reference to Location: rdfID \"").Append(cimTerminal.ConductingEquipment.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.TERMINAL_CONDEQ, gid));
                }

            }
        }

		public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd)
		{
			if ((cimPowerSystemResource != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);

			}
		}

		public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd)
		{
			if ((cimEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd);

			}
		}

		public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimConductingEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateEquipmentProperties(cimConductingEquipment, rd);

				//if (cimConductingEquipment.TerminalsHasValue)
				//{
				//	rd.AddProperty(new Property(ModelCode.EQUIPMENT_ISPRIVATE, cimEquipment.Private));
				//}

			}
		}

        public static void PopulateSeriesCompensatorProperties(FTN.SeriesCompensator cimSeriesCompensator, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimSeriesCompensator != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductingEquipmentProperties(cimSeriesCompensator, rd,importHelper,report);

                if (cimSeriesCompensator.RHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SERSOMP_R, cimSeriesCompensator.R));
                }
                if (cimSeriesCompensator.R0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SERSOMP_R0, cimSeriesCompensator.R0));
                }
                if (cimSeriesCompensator.XHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SERSOMP_X, cimSeriesCompensator.X));
                }
                if (cimSeriesCompensator.X0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SERSOMP_X0, cimSeriesCompensator.X0));
                }

            }
        }

        public static void PopulateConductorProperties(FTN.Conductor cimConductor, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimConductor != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductingEquipmentProperties(cimConductor, rd, importHelper, report);

                if (cimConductor.LengthHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CONDUCTOR_LEN, cimConductor.Length));
                }

            }
        }

        public static void PopulateDCLineSegmentProperties(FTN.DCLineSegment cimDCLineSegment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimDCLineSegment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductorProperties(cimDCLineSegment, rd, importHelper, report);

            }
        }

        public static void PopulateACLineSegmentProperties(FTN.ACLineSegment cimACLineSegment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimACLineSegment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductorProperties(cimACLineSegment, rd, importHelper, report);

                if (cimACLineSegment.B0chHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEG_B0CH, cimACLineSegment.B0ch));
                }
                if (cimACLineSegment.BchHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEG_BCH, cimACLineSegment.Bch));
                }
                if (cimACLineSegment.G0chHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEG_G0CH, cimACLineSegment.G0ch));
                }
                if (cimACLineSegment.GchHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEG_GCH, cimACLineSegment.Gch));
                }
                if (cimACLineSegment.RHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEG_R, cimACLineSegment.R));
                }
                if (cimACLineSegment.R0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEG_R0, cimACLineSegment.R0));
                }
                if (cimACLineSegment.XHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEG_X, cimACLineSegment.X));
                }
                if (cimACLineSegment.X0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEG_X0, cimACLineSegment.X0));
                }

                if (cimACLineSegment.PerLengthImpedanceHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimACLineSegment.PerLengthImpedance.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimACLineSegment.GetType().ToString()).Append(" rdfID = \"").Append(cimACLineSegment.ID);
                        report.Report.Append("\" - Failed to set reference to PowerTransformer: rdfID \"").Append(cimACLineSegment.PerLengthImpedance.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.ACLINESEG_PERLENIMPEDANCE, gid));
                }

            }
        }

        public static void PopulateImpedanceDataProperties(FTN.PhaseImpedanceData cimPhaseImpedanceData, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimPhaseImpedanceData != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPhaseImpedanceData, rd);

                if (cimPhaseImpedanceData.BHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PHASEIMPDATA_B, cimPhaseImpedanceData.B));
                }
                if (cimPhaseImpedanceData.RHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PHASEIMPDATA_R, cimPhaseImpedanceData.R));
                }
                if (cimPhaseImpedanceData.XHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PHASEIMPDATA_X, cimPhaseImpedanceData.X));
                }
                if (cimPhaseImpedanceData.SequenceNumberHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PHASEIMPDATA_SEQNUM, cimPhaseImpedanceData.SequenceNumber));
                }
                if (cimPhaseImpedanceData.PhaseImpedanceHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimPhaseImpedanceData.PhaseImpedance.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimPhaseImpedanceData.GetType().ToString()).Append(" rdfID = \"").Append(cimPhaseImpedanceData.ID);
                        report.Report.Append("\" - Failed to set reference to PowerTransformer: rdfID \"").Append(cimPhaseImpedanceData.PhaseImpedance.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.PHASEIMPDATA_PHASEIMP, gid));
                }
            }
        }

        public static void PopulatePerLengthImpedanceProperties(FTN.PerLengthImpedance cimPerLengthImpedanceData, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimPerLengthImpedanceData != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPerLengthImpedanceData, rd);

                //if (cimPerLengthImpedance.ACLineSegmentsHasValue) {

                //    long gid = importHelper.GetMappedGID(cimPerLengthImpedance.ACLineSegments.ID);
                //    if (gid < 0)
                //    {
                //        report.Report.Append("WARNING: Convert ").Append(cimPerLengthImpedance.GetType().ToString()).Append(" rdfID = \"").Append(cimPerLengthImpedance.ID);
                //        report.Report.Append("\" - Failed to set reference to PowerTransformer: rdfID \"").Append(cimPerLengthImpedance.ACLineSegments.ID).AppendLine(" \" is not mapped to GID!");
                //    }
                //    rd.AddProperty(new Property(ModelCode.PERLENIMPEDANCE_ACLINESEGS, gid));

                //}

            }
        }

        public static void PopulatePerLengthSequenceImpedanceProperties(FTN.PerLengthSequenceImpedance cimPerLengthSequenceImpedanceData, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimPerLengthSequenceImpedanceData != null) && (rd != null))
            {
                PowerTransformerConverter.PopulatePerLengthImpedanceProperties(cimPerLengthSequenceImpedanceData, rd,importHelper,report);

                if (cimPerLengthSequenceImpedanceData.B0chHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMPEDANCE_B0CH, cimPerLengthSequenceImpedanceData.B0ch));
                }
                if (cimPerLengthSequenceImpedanceData.BchHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMPEDANCE_BCH, cimPerLengthSequenceImpedanceData.Bch));
                }
                if (cimPerLengthSequenceImpedanceData.G0chHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMPEDANCE_G0CH, cimPerLengthSequenceImpedanceData.G0ch));
                }
                if (cimPerLengthSequenceImpedanceData.GchHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMPEDANCE_GCH, cimPerLengthSequenceImpedanceData.Gch));
                }
                if (cimPerLengthSequenceImpedanceData.RHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMPEDANCE_R, cimPerLengthSequenceImpedanceData.R));
                }
                if (cimPerLengthSequenceImpedanceData.R0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMPEDANCE_R0, cimPerLengthSequenceImpedanceData.R0));
                }
                if (cimPerLengthSequenceImpedanceData.XHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMPEDANCE_X, cimPerLengthSequenceImpedanceData.X));
                }
                if (cimPerLengthSequenceImpedanceData.X0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMPEDANCE_X0, cimPerLengthSequenceImpedanceData.X0));
                }
            }
        }

        public static void PopulatePerLengthPhaseImpedanceProperties(FTN.PerLengthPhaseImpedance cimPerLengthPhaseImpedance, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimPerLengthPhaseImpedance != null) && (rd != null))
            {
                PowerTransformerConverter.PopulatePerLengthImpedanceProperties(cimPerLengthPhaseImpedance, rd,importHelper,report);

                if (cimPerLengthPhaseImpedance.ConductorCountHasValue)
                {
                    Console.WriteLine("ConductorCount: " + cimPerLengthPhaseImpedance.ConductorCount);

                    rd.AddProperty(new Property(ModelCode.PERLENPHASEIMP_CONDCOUNT,Int32.Parse(cimPerLengthPhaseImpedance.ConductorCount.ToString())));
                }

            }
        }

        #endregion Populate ResourceDescription

        #region Enums convert

        #endregion Enums convert
    }
}
