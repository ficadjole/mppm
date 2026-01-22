using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class PerLengthImpedance : IdentifiedObject
    {

        private List<long> aclinesegments = new List<long>();

        public PerLengthImpedance(long globalId) : base(globalId)
        {
        }

        public List<long> Aclinesegments
        {
            get { return aclinesegments; }
            set { aclinesegments = value; }
        }


        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                PerLengthImpedance x = (PerLengthImpedance)obj;
                return (CompareHelper.CompareLists(x.aclinesegments, this.aclinesegments));
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IAccess implementation

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.PERLENIMPEDANCE_ACLINESEGS:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }


        public override void GetProperty(Property property)
        {
            switch(property.Id)
            {
                case ModelCode.PERLENIMPEDANCE_ACLINESEGS:
                    property.SetValue(aclinesegments);
                    break;
                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                default:
                    base.SetProperty(property);
                    break;
            }
        }
        #endregion IAccess implementation

        #region IReference implementation
        public override bool IsReferenced
        {
            get
            {
                return aclinesegments.Count != 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (aclinesegments != null && aclinesegments.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.PERLENIMPEDANCE_ACLINESEGS] = aclinesegments.GetRange(0, aclinesegments.Count);
            }
            base.GetReferences(references, refType);
        }


        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.ACLINESEG_PERLENIMPEDANCE:
                    aclinesegments.Add(globalId);
                    break;
                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.ACLINESEG_PERLENIMPEDANCE:
                    if (aclinesegments.Contains(globalId))
                    {
                        aclinesegments.Remove(globalId);
                    }
                    else
                    {
                        Console.WriteLine("WARNING: Trying to remove reference that does not exist!");
                    }
                    break;
                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }

        #endregion IReference implementation
    }
}
