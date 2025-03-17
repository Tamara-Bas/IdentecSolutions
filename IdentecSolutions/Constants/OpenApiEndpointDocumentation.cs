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

        public const string CreateEquipmentSummary = "Create equipment";
        public const string CreateEquipmentDescription = @"
### Rules
   - Route POST api/equipment
   - Name is required (string). Max length is 50 characters.
    - Description is required (string). Max length is 200 characters.
    - Serial number is required (string)
    - Price is required (decimal)
    - Location is required. Max length is 50 characters.
    -WarrantyExpiryDate format dd-mm-yyyy
    - EquipmentType is required. Available input 1,2,3
    - Status is required. Defaul value false
    
### Response
    -response is boolean. True if is successfully created, false if it is not. If there is any exception, transaction will be rollback
    - Status code 200";

        public const string UpdateEquipmentSummary = "Update equipment by id";
        public const string UpdateEquipmentDescription = @"
### Rules
    - Route PUT api/equipment/{id}
    - Id is required (int).
    - Status is required
    
### Response
    - Status code 200";

        public const string DeleteEquipmentSummary = "Delete equipment";
        public const string DeleteEquipmentDescription = @"
### Rules
   - Route Delete api/equipment/{id}
   - Id is required
    
### Response
    -response is boolean. True if is successfully deleted, false if it is not. If there is any exception, transaction will be rollback
    - Status code 200";

    }
}
