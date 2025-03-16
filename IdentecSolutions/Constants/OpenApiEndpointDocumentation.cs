namespace IdentecSolutions.WebApi.Constants
{
    public class OpenApiEndpointDocumentation
    {
        public const string GetAllEquipmentByStatusSummary = "Get all equipment by status";
        public const string GetAllEquipmentByStatusDescription = @"
### Rules
   - Route GET api/equipment/get-all-equipment-by-status
   - Status of equipment is required (true or false). Status true means that it will fetched all equipment that is in usage
### Response
    - Status code 200, successfully fetched equipment by status";

        public const string GetEquipmentByIdSummary = "Get equipment by Id";
        public const string GetEquipmentByIdDescription = @"
### Rules
   - Route GET api/equipment/get-equipment-by-id
   - Id of equipment is required
### Response
    - Status code 200, successfully fetched equipment by id";

    }
}
