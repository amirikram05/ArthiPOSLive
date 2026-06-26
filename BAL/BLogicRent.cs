

using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BAL
{
    public class BLogicRent
    {
        private readonly SqlDataService _dataService;

        public BLogicRent()
        {
            _dataService = new SqlDataService();
        }

        #region Property Business Logic

        public List<Property> GetAllProperties()
        {
            try
            {
                return _dataService.LoadProperties();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving properties: " + ex.Message);
            }
        }

        public Property GetPropertyById(int propertyId)
        {
            try
            {
                var properties = _dataService.LoadProperties();
                return properties.FirstOrDefault(p => p.Id == propertyId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving property: " + ex.Message);
            }
        }

        public bool AddProperty(Property property)
        {
            try
            {
                ValidateProperty(property);
                var properties = _dataService.LoadProperties();

                // Check for duplicate name
                if (properties.Any(p => p.Name.Equals(property.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception($"Property with name '{property.Name}' already exists.");
                }

                property.Id = 0; // Ensure new ID
                properties.Add(property);
                _dataService.SaveProperties(properties);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding property: " + ex.Message);
            }
        }

        public bool UpdateProperty(Property property)
        {
            try
            {
                ValidateProperty(property);
                var properties = _dataService.LoadProperties();
                var existingProperty = properties.FirstOrDefault(p => p.Id == property.Id);

                if (existingProperty == null)
                    throw new Exception("Property not found.");

                // Check for duplicate name excluding current property
                if (properties.Any(p => p.Id != property.Id &&
                    p.Name.Equals(property.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception($"Property with name '{property.Name}' already exists.");
                }

                existingProperty.Name = property.Name;
                existingProperty.Address = property.Address;
                existingProperty.Type = property.Type;

                _dataService.SaveProperties(properties);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating property: " + ex.Message);
            }
        }

        public bool DeleteProperty(int propertyId)
        {
            try
            {
                var properties = _dataService.LoadProperties();
                var portions = _dataService.LoadPortions();
                var agreements = _dataService.LoadAgreements();

                // Check if property has portions
                if (portions.Any(p => p.PropertyId == propertyId))
                {
                    throw new Exception("Cannot delete property with existing portions. Delete portions first.");
                }

                // Check if property has agreements
                if (agreements.Any(a => a.PropertyId == propertyId))
                {
                    throw new Exception("Cannot delete property with existing agreements. Delete agreements first.");
                }

                properties.RemoveAll(p => p.Id == propertyId);
                _dataService.SaveProperties(properties);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting property: " + ex.Message);
            }
        }

        private void ValidateProperty(Property property)
        {
            if (string.IsNullOrWhiteSpace(property.Name))
                throw new Exception("Property name is required.");

            if (property.Name.Length > 200)
                throw new Exception("Property name cannot exceed 200 characters.");

            if (!string.IsNullOrEmpty(property.Address) && property.Address.Length > 500)
                throw new Exception("Address cannot exceed 500 characters.");
        }

        #endregion

        #region Portion Business Logic

        public List<Portion> GetAllPortions()
        {
            try
            {
                return _dataService.LoadPortions();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving portions: " + ex.Message);
            }
        }

        public List<Portion> GetPortionsByProperty(int propertyId)
        {
            try
            {
                var portions = _dataService.LoadPortions();
                return portions.Where(p => p.PropertyId == propertyId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving portions: " + ex.Message);
            }
        }

        public Portion GetPortionById(int portionId)
        {
            try
            {
                var portions = _dataService.LoadPortions();
                return portions.FirstOrDefault(p => p.Id == portionId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving portion: " + ex.Message);
            }
        }

        public bool AddPortion(Portion portion)
        {
            try
            {
                ValidatePortion(portion);
                var portions = _dataService.LoadPortions();

                // Check for duplicate portion name in same property
                if (portions.Any(p => p.PropertyId == portion.PropertyId &&
                    p.Name.Equals(portion.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception($"Portion with name '{portion.Name}' already exists in this property.");
                }

                portion.Id = 0;
                portions.Add(portion);
                _dataService.SavePortions(portions);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding portion: " + ex.Message);
            }
        }

        public bool UpdatePortion(Portion portion)
        {
            try
            {
                ValidatePortion(portion);
                var portions = _dataService.LoadPortions();
                var existingPortion = portions.FirstOrDefault(p => p.Id == portion.Id);

                if (existingPortion == null)
                    throw new Exception("Portion not found.");

                // Check for duplicate portion name in same property excluding current
                if (portions.Any(p => p.Id != portion.Id && p.PropertyId == portion.PropertyId &&
                    p.Name.Equals(portion.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception($"Portion with name '{portion.Name}' already exists in this property.");
                }

                existingPortion.PropertyId = portion.PropertyId;
                existingPortion.Name = portion.Name;
                existingPortion.Size = portion.Size;
                existingPortion.IsActive = portion.IsActive;

                _dataService.SavePortions(portions);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating portion: " + ex.Message);
            }
        }

        public bool DeletePortion(int portionId)
        {
            try
            {
                var portions = _dataService.LoadPortions();
                var agreements = _dataService.LoadAgreements();

                // Check if portion has agreements
                if (agreements.Any(a => a.PortionId == portionId))
                {
                    throw new Exception("Cannot delete portion with existing agreements. Delete agreements first.");
                }

                portions.RemoveAll(p => p.Id == portionId);
                _dataService.SavePortions(portions);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting portion: " + ex.Message);
            }
        }

        private void ValidatePortion(Portion portion)
        {
            if (portion.PropertyId <= 0)
                throw new Exception("Please select a valid property.");

            if (string.IsNullOrWhiteSpace(portion.Name))
                throw new Exception("Portion name is required.");

            if (portion.Name.Length > 200)
                throw new Exception("Portion name cannot exceed 200 characters.");

            if (string.IsNullOrWhiteSpace(portion.Size))
                throw new Exception("Portion size is required.");

            if (portion.Size.Length > 50)
                throw new Exception("Size cannot exceed 50 characters.");
        }

        #endregion

        #region Tenant Business Logic

        public List<Tenant> GetAllTenants()
        {
            try
            {
                return _dataService.LoadTenants();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving tenants: " + ex.Message);
            }
        }

        public List<Tenant> GetTenantsByType(TenantType type)
        {
            try
            {
                var tenants = _dataService.LoadTenants();
                return tenants.Where(t => t.Type == type).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving tenants: " + ex.Message);
            }
        }

        public Tenant GetTenantById(int tenantId)
        {
            try
            {
                var tenants = _dataService.LoadTenants();
                return tenants.FirstOrDefault(t => t.Id == tenantId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving tenant: " + ex.Message);
            }
        }

        public bool AddTenant(Tenant tenant)
        {
            try
            {
                ValidateTenant(tenant);
                var tenants = _dataService.LoadTenants();

                // Check for duplicate CNIC
                if (!string.IsNullOrWhiteSpace(tenant.CNIC) &&
                    tenants.Any(t => t.CNIC?.Equals(tenant.CNIC, StringComparison.OrdinalIgnoreCase) == true))
                {
                    throw new Exception($"Tenant with CNIC '{tenant.CNIC}' already exists.");
                }

                // Check for duplicate mobile
                if (!string.IsNullOrWhiteSpace(tenant.Mobile) &&
                    tenants.Any(t => t.Mobile?.Equals(tenant.Mobile) == true))
                {
                    throw new Exception($"Tenant with mobile '{tenant.Mobile}' already exists.");
                }

                tenant.Id = 0;
                tenant.CreatedDate = DateTime.Now;
                tenants.Add(tenant);
                _dataService.SaveTenants(tenants);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding tenant: " + ex.Message);
            }
        }

        public bool UpdateTenant(Tenant tenant)
        {
            try
            {
                ValidateTenant(tenant);
                var tenants = _dataService.LoadTenants();
                var existingTenant = tenants.FirstOrDefault(t => t.Id == tenant.Id);

                if (existingTenant == null)
                    throw new Exception("Tenant not found.");

                // Check for duplicate CNIC excluding current
                if (!string.IsNullOrWhiteSpace(tenant.CNIC) &&
                    tenants.Any(t => t.Id != tenant.Id && t.CNIC?.Equals(tenant.CNIC, StringComparison.OrdinalIgnoreCase) == true))
                {
                    throw new Exception($"Tenant with CNIC '{tenant.CNIC}' already exists.");
                }

                // Check for duplicate mobile excluding current
                if (!string.IsNullOrWhiteSpace(tenant.Mobile) &&
                    tenants.Any(t => t.Id != tenant.Id && t.Mobile?.Equals(tenant.Mobile) == true))
                {
                    throw new Exception($"Tenant with mobile '{tenant.Mobile}' already exists.");
                }

                existingTenant.Name = tenant.Name;
                existingTenant.CNIC = tenant.CNIC;
                existingTenant.Mobile = tenant.Mobile;
                existingTenant.SecurityDeposit = tenant.SecurityDeposit;
                existingTenant.StampPaperDetails = tenant.StampPaperDetails;
                existingTenant.StampPaperDate = tenant.StampPaperDate;
                existingTenant.Type = tenant.Type;
                existingTenant.CommissionPercentage = tenant.CommissionPercentage;
                existingTenant.CommissionFrequency = tenant.CommissionFrequency;
                existingTenant.CustomCommissionDays = tenant.CustomCommissionDays;

                _dataService.SaveTenants(tenants);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating tenant: " + ex.Message);
            }
        }

        public bool DeleteTenant(int tenantId)
        {
            try
            {
                var tenants = _dataService.LoadTenants();
                var agreements = _dataService.LoadAgreements();

                // Check if tenant has agreements
                if (agreements.Any(a => a.TenantId == tenantId))
                {
                    throw new Exception("Cannot delete tenant with existing agreements. Delete agreements first.");
                }

                tenants.RemoveAll(t => t.Id == tenantId);
                _dataService.SaveTenants(tenants);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting tenant: " + ex.Message);
            }
        }

        private void ValidateTenant(Tenant tenant)
        {
            if (string.IsNullOrWhiteSpace(tenant.Name))
                throw new Exception("Tenant name is required.");

            if (tenant.Name.Length > 200)
                throw new Exception("Tenant name cannot exceed 200 characters.");

            if (!string.IsNullOrEmpty(tenant.CNIC) && tenant.CNIC.Length > 50)
                throw new Exception("CNIC cannot exceed 50 characters.");

            if (!string.IsNullOrEmpty(tenant.Mobile) && tenant.Mobile.Length > 20)
                throw new Exception("Mobile number cannot exceed 20 characters.");

            if (tenant.SecurityDeposit < 0)
                throw new Exception("Security deposit cannot be negative.");

            if (tenant.Type == TenantType.OnCommission)
            {
                if (!tenant.CommissionPercentage.HasValue || tenant.CommissionPercentage.Value <= 0)
                    throw new Exception("Commission percentage is required for commission tenants.");

                if (tenant.CommissionPercentage.Value > 100)
                    throw new Exception("Commission percentage cannot exceed 100%.");

                if (!tenant.CommissionFrequency.HasValue)
                    throw new Exception("Commission frequency is required for commission tenants.");
            }
        }

        #endregion

        #region Product Business Logic

        public List<Product> GetAllProducts()
        {
            try
            {
                return _dataService.LoadProducts();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving products: " + ex.Message);
            }
        }

        public List<Product> GetActiveProducts()
        {
            try
            {
                var products = _dataService.LoadProducts();
                return products.Where(p => p.IsActive).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving active products: " + ex.Message);
            }
        }

        public Product GetProductById(int productId)
        {
            try
            {
                var products = _dataService.LoadProducts();
                return products.FirstOrDefault(p => p.Id == productId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving product: " + ex.Message);
            }
        }

        public bool AddProduct(Product product)
        {
            try
            {
                ValidateProduct(product);
                var products = _dataService.LoadProducts();

                // Check for duplicate name
                if (products.Any(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception($"Product with name '{product.Name}' already exists.");
                }

                product.Id = 0;
                product.CreatedDate = DateTime.Now;
                products.Add(product);
                _dataService.SaveProducts(products);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding product: " + ex.Message);
            }
        }

        public bool UpdateProduct(Product product)
        {
            try
            {
                ValidateProduct(product);
                var products = _dataService.LoadProducts();
                var existingProduct = products.FirstOrDefault(p => p.Id == product.Id);

                if (existingProduct == null)
                    throw new Exception("Product not found.");

                // Check for duplicate name excluding current
                if (products.Any(p => p.Id != product.Id &&
                    p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception($"Product with name '{product.Name}' already exists.");
                }

                existingProduct.Name = product.Name;
                existingProduct.Unit = product.Unit;
                existingProduct.UnitPrice = product.UnitPrice;
                existingProduct.IsActive = product.IsActive;

                _dataService.SaveProducts(products);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating product: " + ex.Message);
            }
        }

        public bool DeleteProduct(int productId)
        {
            try
            {
                var products = _dataService.LoadProducts();
                var agreements = _dataService.LoadAgreements();

                // Check if product is used in any agreement
                if (agreements.Any(a => a.ProductIds != null && a.ProductIds.Contains(productId)))
                {
                    throw new Exception("Cannot delete product as it is assigned to one or more agreements. Please remove it from agreements first or mark it as inactive.");
                }

                products.RemoveAll(p => p.Id == productId);
                _dataService.SaveProducts(products);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting product: " + ex.Message);
            }
        }

        private void ValidateProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new Exception("Product name is required.");

            if (product.Name.Length > 200)
                throw new Exception("Product name cannot exceed 200 characters.");

            if (string.IsNullOrWhiteSpace(product.Unit))
                throw new Exception("Unit is required (e.g., kg, piece, box).");

            if (product.Unit.Length > 50)
                throw new Exception("Unit cannot exceed 50 characters.");

            if (product.UnitPrice <= 0)
                throw new Exception("Unit price must be greater than zero.");

            if (product.UnitPrice > 1000000)
                throw new Exception("Unit price cannot exceed 1,000,000.");
        }

        #endregion

        #region Rent Agreement Business Logic

        public List<RentAgreement> GetAllAgreements()
        {
            try
            {
                return _dataService.LoadAgreements();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving agreements: " + ex.Message);
            }
        }

        public List<RentAgreement> GetActiveAgreements()
        {
            try
            {
                var agreements = _dataService.LoadAgreements();
                return agreements.Where(a => a.IsActive).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving active agreements: " + ex.Message);
            }
        }

        public RentAgreement GetAgreementById(int agreementId)
        {
            try
            {
                var agreements = _dataService.LoadAgreements();
                return agreements.FirstOrDefault(a => a.Id == agreementId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving agreement: " + ex.Message);
            }
        }

        public List<RentAgreement> GetAgreementsByTenant(int tenantId)
        {
            try
            {
                var agreements = _dataService.LoadAgreements();
                return agreements.Where(a => a.TenantId == tenantId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving tenant agreements: " + ex.Message);
            }
        }

        public List<RentAgreement> GetAgreementsByProperty(int propertyId)
        {
            try
            {
                var agreements = _dataService.LoadAgreements();
                return agreements.Where(a => a.PropertyId == propertyId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving property agreements: " + ex.Message);
            }
        }

        public bool AddRentAgreement(RentAgreement agreement)
        {
            try
            {
                ValidateRentAgreement(agreement);
                var agreements = _dataService.LoadAgreements();

                // Check if portion is already occupied by active agreement
                if (agreements.Any(a => a.PortionId == agreement.PortionId && a.IsActive))
                {
                    throw new Exception("This portion is already occupied by an active agreement.");
                }

                // Check if tenant already has active agreement for different portion
                if (agreements.Any(a => a.TenantId == agreement.TenantId && a.IsActive))
                {
                    var existingAgreement = agreements.First(a => a.TenantId == agreement.TenantId && a.IsActive);
                    throw new Exception($"Tenant already has an active agreement for portion ID: {existingAgreement.PortionId}");
                }

                agreement.Id = 0;
                agreement.LastIncreaseDate = agreement.StartDate;
                agreement.NextDueDate = agreement.StartDate.AddMonths(1);
                agreement.LastCommissionPaymentDate = agreement.StartDate;

                agreements.Add(agreement);
                _dataService.SaveAgreements(agreements);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding rent agreement: " + ex.Message);
            }
        }

        public bool UpdateRentAgreement(RentAgreement agreement)
        {
            try
            {
                ValidateRentAgreement(agreement);
                var agreements = _dataService.LoadAgreements();
                var existingAgreement = agreements.FirstOrDefault(a => a.Id == agreement.Id);

                if (existingAgreement == null)
                    throw new Exception("Agreement not found.");

                // Check if portion is occupied by another active agreement
                if (agreement.IsActive &&
                    agreements.Any(a => a.Id != agreement.Id && a.PortionId == agreement.PortionId && a.IsActive))
                {
                    throw new Exception("This portion is already occupied by another active agreement.");
                }

                existingAgreement.PropertyId = agreement.PropertyId;
                existingAgreement.PortionId = agreement.PortionId;
                existingAgreement.TenantId = agreement.TenantId;
                existingAgreement.MonthlyRent = agreement.MonthlyRent;
                existingAgreement.StartDate = agreement.StartDate;
                existingAgreement.IncreaseMode = agreement.IncreaseMode;
                existingAgreement.IsActive = agreement.IsActive;
                existingAgreement.DailyMinimumTarget = agreement.DailyMinimumTarget;
                existingAgreement.CommissionRate = agreement.CommissionRate;
                existingAgreement.PaymentFrequency = agreement.PaymentFrequency;
                existingAgreement.CustomPaymentDays = agreement.CustomPaymentDays;
                existingAgreement.ProductIds = agreement.ProductIds;

                _dataService.SaveAgreements(agreements);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating rent agreement: " + ex.Message);
            }
        }

        public bool TerminateAgreement(int agreementId)
        {
            try
            {
                var agreements = _dataService.LoadAgreements();
                var agreement = agreements.FirstOrDefault(a => a.Id == agreementId);

                if (agreement == null)
                    throw new Exception("Agreement not found.");

                agreement.IsActive = false;
                _dataService.SaveAgreements(agreements);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error terminating agreement: " + ex.Message);
            }
        }

        private void ValidateRentAgreement(RentAgreement agreement)
        {
            if (agreement.PropertyId <= 0)
                throw new Exception("Please select a valid property.");

            if (agreement.PortionId <= 0)
                throw new Exception("Please select a valid portion.");

            if (agreement.TenantId <= 0)
                throw new Exception("Please select a valid tenant.");

            if (agreement.MonthlyRent <= 0)
                throw new Exception("Monthly rent must be greater than zero.");

            if (agreement.MonthlyRent > 10000000)
                throw new Exception("Monthly rent cannot exceed 10,000,000.");

            if (agreement.StartDate == DateTime.MinValue)
                throw new Exception("Please select a valid start date.");

            if (agreement.StartDate > DateTime.Now.AddMonths(1))
                throw new Exception("Start date cannot be more than 1 month in the future.");

            var tenant = GetTenantById(agreement.TenantId);
            if (tenant == null)
                throw new Exception("Selected tenant not found.");

            // Validate commission-specific fields
            if (tenant.Type == TenantType.OnCommission)
            {
                if (!agreement.CommissionRate.HasValue || agreement.CommissionRate <= 0)
                    throw new Exception("Commission rate is required for commission tenants.");

                if (!agreement.PaymentFrequency.HasValue)
                    throw new Exception("Payment frequency is required for commission tenants.");
            }
        }

        #endregion

        #region Payment Business Logic

        public List<Payment> GetAllPayments()
        {
            try
            {
                return _dataService.LoadPayments();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving payments: " + ex.Message);
            }
        }

        public List<Payment> GetPaymentsByAgreement(int agreementId)
        {
            try
            {
                return _dataService.GetPaymentsByAgreement(agreementId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving agreement payments: " + ex.Message);
            }
        }

        public List<Payment> GetPaymentsByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var payments = _dataService.LoadPayments();
                return payments.Where(p => p.PaymentDate.Date >= startDate.Date &&
                                          p.PaymentDate.Date <= endDate.Date)
                              .OrderByDescending(p => p.PaymentDate)
                              .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving payments by date range: " + ex.Message);
            }
        }

        public bool RecordRentPayment(int agreementId, decimal amount, DateTime paymentDate, string notes = "")
        {
            try
            {
                var agreements = _dataService.LoadAgreements();
                var agreement = agreements.FirstOrDefault(a => a.Id == agreementId);

                if (agreement == null)
                    throw new Exception("Agreement not found.");

                if (!agreement.IsActive)
                    throw new Exception("Cannot record payment for inactive agreement.");

                if (amount <= 0)
                    throw new Exception("Payment amount must be greater than zero.");

                var payments = _dataService.LoadAllPayments();

                var payment = new Payment
                {
                    AgreementId = agreementId,
                    Amount = amount,
                    PaymentDate = paymentDate,
                    MonthYear = paymentDate.ToString("MMMM yyyy"),
                    Notes = notes,
                    PaymentType = PaymentType.Rent,
                    CreatedDate = DateTime.Now
                };

                payment.Id = payments.Count > 0 ? payments.Max(p => p.Id) + 1 : 1;
                payments.Add(payment);

                // Update agreement next due date
                agreement.NextDueDate = paymentDate.AddMonths(1);

                _dataService.SavePayments(payments);
                _dataService.SaveAgreements(agreements);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error recording rent payment: " + ex.Message);
            }
        }

        public bool RecordCommissionPayment(int agreementId, List<CommissionTransaction> transactions,
                                           decimal laborAmount, string notes = "")
        {
            try
            {
                var agreements = _dataService.LoadAgreements();
                var agreement = agreements.FirstOrDefault(a => a.Id == agreementId);

                if (agreement == null)
                    throw new Exception("Agreement not found.");

                if (!agreement.IsActive)
                    throw new Exception("Cannot record payment for inactive agreement.");

                if (!agreement.CommissionRate.HasValue)
                    throw new Exception("Commission rate not set for this agreement.");

                if (transactions == null || !transactions.Any())
                    throw new Exception("At least one product transaction is required.");

                // Calculate totals
                decimal totalProductValue = transactions.Sum(t => t.ProductTotal);
                decimal totalCommission = transactions.Sum(t => t.CommissionAmount);
                decimal totalAmount = totalProductValue + laborAmount;

                var payments = _dataService.LoadAllPayments();
                var allTransactions = _dataService.LoadCommissionTransactions();

                // Create payment record
                var payment = new Payment
                {
                    AgreementId = agreementId,
                    Amount = totalAmount,
                    PaymentDate = DateTime.Now,
                    MonthYear = DateTime.Now.ToString("MMMM yyyy"),
                    Notes = notes,
                    PaymentType = PaymentType.Commission,
                    SalesAmount = totalProductValue,
                    CommissionEarned = totalCommission,
                    LaborAmount = laborAmount,
                    CreatedDate = DateTime.Now
                };

                payment.Id = payments.Count > 0 ? payments.Max(p => p.Id) + 1 : 1;
                payments.Add(payment);

                // Save commission transactions
                foreach (var transaction in transactions)
                {
                    transaction.Id = 0;
                    transaction.PaymentId = payment.Id;
                    transaction.TransactionDate = DateTime.Now;
                    allTransactions.Add(transaction);
                }

                // Update agreement last commission payment date
                agreement.LastCommissionPaymentDate = DateTime.Now;

                _dataService.SavePayments(payments);
                _dataService.SaveCommissionTransactions(allTransactions);
                _dataService.SaveAgreements(agreements);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error recording commission payment: " + ex.Message);
            }
        }

        public bool DeletePayment(int paymentId, string reason = "")
        {
            try
            {
                return _dataService.DeletePayment(paymentId, reason);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting payment: " + ex.Message);
            }
        }

        public bool RestorePayment(int paymentId)
        {
            try
            {
                return _dataService.RestorePayment(paymentId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error restoring payment: " + ex.Message);
            }
        }

        #endregion

        #region Commission Business Logic

        public CommissionTransaction CalculateCommission(int productId, decimal quantity, decimal laborAmount, decimal commissionRate)
        {
            try
            {
                if (quantity <= 0)
                    throw new Exception("Quantity must be greater than zero.");

                if (laborAmount < 0)
                    throw new Exception("Labor amount cannot be negative.");

                if (commissionRate <= 0 || commissionRate > 100)
                    throw new Exception("Commission rate must be between 0 and 100.");

                return _dataService.CalculateCommission(productId, quantity, laborAmount, commissionRate);
            }
            catch (Exception ex)
            {
                throw new Exception("Error calculating commission: " + ex.Message);
            }
        }

        public List<ProductSelection> GetProductSelectionsForAgreement(int agreementId)
        {
            try
            {
                return _dataService.GetProductSelectionsForAgreement(agreementId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving product selections: " + ex.Message);
            }
        }

        public List<Product> GetProductsForAgreement(int agreementId)
        {
            try
            {
                return _dataService.GetProductsForAgreement(agreementId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving products for agreement: " + ex.Message);
            }
        }

        public CommissionSummary GetCommissionSummary(int paymentId)
        {
            try
            {
                return _dataService.GetCommissionSummary(paymentId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving commission summary: " + ex.Message);
            }
        }

        public List<CommissionTransaction> GetCommissionTransactionsForPayment(int paymentId)
        {
            try
            {
                return _dataService.GetCommissionTransactionsForPayment(paymentId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving commission transactions: " + ex.Message);
            }
        }

        #endregion

        #region Report Business Logic

        public List<RentOverview> GetRentOverviews()
        {
            try
            {
                var overviews = new List<RentOverview>();
                var agreements = GetActiveAgreements();
                var properties = GetAllProperties();
                var portions = GetAllPortions();
                var tenants = GetAllTenants();
                var payments = GetAllPayments();

                foreach (var agreement in agreements)
                {
                    var property = properties.FirstOrDefault(p => p.Id == agreement.PropertyId);
                    var portion = portions.FirstOrDefault(p => p.Id == agreement.PortionId);
                    var tenant = tenants.FirstOrDefault(t => t.Id == agreement.TenantId);

                    if (property == null || portion == null || tenant == null)
                        continue;

                    var agreementPayments = payments
                        .Where(p => p.AgreementId == agreement.Id)
                        .OrderByDescending(p => p.PaymentDate)
                        .ToList();

                    var lastPayment = agreementPayments.FirstOrDefault();

                    if (tenant.Type == TenantType.OnRent)
                    {
                        int monthsPassed = (DateTime.Now.Year - agreement.StartDate.Year) * 12 +
                                          DateTime.Now.Month - agreement.StartDate.Month;
                        monthsPassed = Math.Max(0, monthsPassed);

                        decimal totalRentDue = 0;
                        DateTime currentDate = agreement.StartDate;
                        decimal currentRent = agreement.MonthlyRent;

                        for (int i = 0; i <= monthsPassed; i++)
                        {
                            if (i > 0)
                            {
                                totalRentDue += currentRent;
                            }
                            currentDate = currentDate.AddMonths(1);
                        }

                        decimal totalPaid = agreementPayments.Sum(p => p.Amount);
                        decimal dueAmount = Math.Max(0, totalRentDue - totalPaid);
                        DateTime nextDueDate = agreement.StartDate.AddMonths(monthsPassed + 1);

                        overviews.Add(new RentOverview
                        {
                            PropertyName = property.Name,
                            PortionName = portion.Name,
                            PortionSize = portion.Size,
                            TenantName = tenant.Name,
                            Mobile = tenant.Mobile,
                            MonthlyRent = currentRent,
                            LastPaidAmount = lastPayment?.Amount ?? 0,
                            DueAmount = dueAmount,
                            LastPaymentDate = lastPayment?.PaymentDate ?? DateTime.MinValue,
                            NextDueDate = nextDueDate,
                            AgreementId = agreement.Id,
                            TenantId = tenant.Id,
                            PropertyId = property.Id,
                            PortionId = portion.Id,
                            TenantType = tenant.Type,
                            DaysOverdue = dueAmount > 0 && nextDueDate < DateTime.Now ?
                                (DateTime.Now - nextDueDate).Days : 0
                        });
                    }
                }

                return overviews;
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating rent overviews: " + ex.Message);
            }
        }

        public MonthlySummary GenerateMonthlySummary(int year, int month)
        {
            try
            {
                var summary = new MonthlySummary
                {
                    MonthYear = new DateTime(year, month, 1).ToString("MMMM yyyy"),
                    MonthStart = new DateTime(year, month, 1),
                    MonthEnd = new DateTime(year, month, DateTime.DaysInMonth(year, month)),
                    PropertySummaries = new List<MonthlyPropertySummary>()
                };

                var properties = GetAllProperties();
                var portions = GetAllPortions();
                var tenants = GetAllTenants();
                var agreements = GetActiveAgreements();
                var payments = GetAllPayments()
                    .Where(p => p.PaymentDate.Year == year && p.PaymentDate.Month == month)
                    .ToList();

                summary.TotalProperties = properties.Count;
                summary.TotalTenants = tenants.Count;
                summary.TotalRentTenants = tenants.Count(t => t.Type == TenantType.OnRent);
                summary.TotalCommissionTenants = tenants.Count(t => t.Type == TenantType.OnCommission);

                summary.TotalRentCollected = payments
                    .Where(p => p.PaymentType == PaymentType.Rent)
                    .Sum(p => p.Amount);

                summary.TotalCommissionCollected = payments
                    .Where(p => p.PaymentType == PaymentType.Commission)
                    .Sum(p => p.Amount);

                summary.TotalCollected = summary.TotalRentCollected + summary.TotalCommissionCollected;

                // Calculate dues
                foreach (var agreement in agreements)
                {
                    var tenant = tenants.FirstOrDefault(t => t.Id == agreement.TenantId);
                    if (tenant == null) continue;

                    if (tenant.Type == TenantType.OnRent)
                    {
                        var rentPaid = payments.Any(p => p.AgreementId == agreement.Id && p.PaymentType == PaymentType.Rent);
                        if (!rentPaid)
                        {
                            summary.TotalRentDue += agreement.MonthlyRent;
                        }
                    }
                }

                summary.TotalDue = summary.TotalRentDue + summary.TotalCommissionDue;
                summary.CollectionEfficiency = summary.TotalCollected > 0 ?
                    (summary.TotalCollected * 100 / (summary.TotalCollected + summary.TotalDue)) : 100;

                return summary;
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating monthly summary: " + ex.Message);
            }
        }

        public DueReport GenerateDueReport()
        {
            try
            {
                var report = new DueReport
                {
                    ReportDate = DateTime.Now,
                    DueTenants = new List<DueTenant>(),
                    DueProperties = new List<DueProperty>()
                };

                var overviews = GetRentOverviews();
                var dueOverviews = overviews.Where(o => o.DueAmount > 0).ToList();

                report.TotalDueTenants = dueOverviews.Count;
                report.TotalDueAmount = dueOverviews.Sum(d => d.DueAmount);

                foreach (var overview in dueOverviews)
                {
                    var status = overview.DaysOverdue > 30 ? "Severely Overdue" :
                                overview.DaysOverdue > 7 ? "Overdue" :
                                overview.DaysOverdue > 0 ? "Due Soon" : "Current";

                    report.DueTenants.Add(new DueTenant
                    {
                        TenantName = overview.TenantName,
                        Mobile = overview.Mobile,
                        PropertyName = overview.PropertyName,
                        PortionName = overview.PortionName,
                        TenantType = overview.TenantType,
                        DueAmount = overview.DueAmount,
                        LastPaymentDate = overview.LastPaymentDate,
                        NextDueDate = overview.NextDueDate,
                        DaysOverdue = overview.DaysOverdue,
                        Status = status
                    });
                }

                var propertyGroups = dueOverviews.GroupBy(d => d.PropertyName);
                foreach (var group in propertyGroups)
                {
                    report.DueProperties.Add(new DueProperty
                    {
                        PropertyName = group.Key,
                        DueTenantsCount = group.Count(),
                        TotalDueAmount = group.Sum(d => d.DueAmount),
                        AverageDuePerTenant = group.Average(d => d.DueAmount)
                    });
                }

                return report;
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating due report: " + ex.Message);
            }
        }

        #endregion

        #region Statistics Business Logic

        public Dictionary<string, object> GetDashboardStatistics()
        {
            try
            {
                var stats = new Dictionary<string, object>();
                var properties = GetAllProperties();
                var tenants = GetAllTenants();
                var agreements = GetActiveAgreements();
                var payments = GetAllPayments();
                var currentMonthPayments = payments.Where(p => p.PaymentDate.Month == DateTime.Now.Month).ToList();

                stats["TotalProperties"] = properties.Count;
                stats["TotalTenants"] = tenants.Count;
                stats["RentTenants"] = tenants.Count(t => t.Type == TenantType.OnRent);
                stats["CommissionTenants"] = tenants.Count(t => t.Type == TenantType.OnCommission);
                stats["ActiveAgreements"] = agreements.Count;
                stats["CurrentMonthCollection"] = currentMonthPayments.Sum(p => p.Amount);
                stats["CurrentMonthTransactions"] = currentMonthPayments.Count;
                stats["TotalPayments"] = payments.Count;
                stats["TotalRevenue"] = payments.Sum(p => p.Amount);

                // Occupancy rate
                var portions = GetAllPortions();
                stats["TotalPortions"] = portions.Count;
                stats["OccupiedPortions"] = agreements.Count;
                stats["OccupancyRate"] = portions.Count > 0 ?
                    (agreements.Count * 100.0 / portions.Count) : 0;

                // Due statistics
                var dueTenants = GetRentOverviews().Where(o => o.DueAmount > 0).ToList();
                stats["DueTenants"] = dueTenants.Count;
                stats["TotalDue"] = dueTenants.Sum(d => d.DueAmount);

                return stats;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving dashboard statistics: " + ex.Message);
            }
        }

        public IEnumerable<object> LoadProperties()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> LoadPortions()
        {
            throw new NotImplementedException();
        }

        public object LoadTenants()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> LoadAgreements()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> LoadPayments()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
