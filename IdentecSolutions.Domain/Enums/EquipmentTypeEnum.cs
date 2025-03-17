using System.ComponentModel;

namespace IdentecSolutions.Domain.Enums
{
    public enum EquipmentTypeEnum
    {

        [Description("Internal")]
        Internal=1,

        [Description("Outdoor")]
        Outdoor =2,

        [Description("Mountain")]
        Mountain =3,

        [Description("OutOfSync")]
        OutOfSync = 0
    }
}
