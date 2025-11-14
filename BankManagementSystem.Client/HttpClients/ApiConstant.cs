namespace BankManagementSystem.Client.HttpClients
{
    public static class ApiConstant
    {
        #region
        public const string GetStudentById = "Student/GetStudentById";
        public const string GetAllStudents = "Student/GetAllStudents";
        public const string UpdateStudent = "Student/UpdateStudent";
        public const string DeleteStudent = "Student/DeleteStudent";
        public const string AddStudent = "Student/AddStudent";
        #endregion

        #region Account
        public const string Register = "Account/Register";
        public const string ChangePassword = "Account/ChangePassword";
        #endregion

        #region Token
        public const string Authenticate = "Token/GetToken";
        #endregion


        #region Customer
        public const string GetCustomerById = "Customer/GetCustomerById";
        public const string GetAllCustomers = "Customer/GetAllCustomers";
        public const string UpdateCustomer = "Customer/UpdateCustomer";
        public const string DeleteCustomer = "Customer/DeleteCustomer";
        public const string AddCustomer = "Customer/AddCustomer";       
        public const string GetCustomerBySearch = "Customer/GetCustomerBySearch";
        public const string CustomerRegistrationRequest = "Customer/Register";
        #endregion

        #region Employee
        public const string GetEmployeeById = "Employee/GetEmployeeById";
        public const string GetAllEmployees = "Employee/GetAllEmployees";
        public const string GetAllEmployeesBySearch = "Employee/GetAllEmployeesBySearch";
        public const string UpdateEmployee = "Employee/UpdateEmployee";
        public const string DeleteEmployee = "Employee/DeleteEmployee";
        #endregion

        #region BankAccount
        public const string GetAccountById = "BankAccount/GetAccountById";
        public const string GetAllAccounts = "BankAccount/GetAllAccounts";
        public const string UpdateAccount = "BankAccount/UpdateAccount";
        public const string DeleteAccount = "BankAccount/DeleteAccount";
        public const string AddAccount = "BankAccount/AddAccount";
        public const string RequestAccount = "BankAccount/RequestAccount";
        public const string GetAccountsByCustomer = "BankAccount/GetByCustomer";
        public const string GetAccountByAccountNumber = "BankAccount/GetAccountByAccountNumber";
        #endregion

        #region AccountType
        public const string GetAccountTypeById = "AccountType/GetAccountTypeById";
        public const string GetAllAccountTypes = "AccountType/GetAllAccountTypes";
        public const string UpdateAccountType = "AccountType/UpdateAccountType";
        public const string DeleteAccountType = "AccountType/DeleteAccountType";
        public const string AddAccountType = "AccountType/AddAccountType";
        #endregion

        #region Transaction
        public const string GetTransactionById = "Transaction/GetTransactionById";
        public const string GetAllTransactions = "Transaction/GetAllTransactions";
        public const string UpdateTransaction = "Transaction/UpdateTransaction";
        public const string DeleteTransaction = "Transaction/DeleteTransaction";
        public const string AddTransaction = "Transaction/AddTransaction";
        public const string Deposit = "Transaction/Deposit";
        public const string Withdraw = "Transaction/Withdraw";
        public const string GetTransactionByAccount = "Transaction/GetTransactionsByAccount";
        public const string Transfer = "Transaction/Transfer";
        public const string GetTransactionsBySearch = "Transaction/GetTransactionsBySearch";
        public const string GetTransactionsByCustomerId = "Transaction/GetTransactionsByCustomerId";
        #endregion

        #region Dashboard
        public const string GetDashboard = "Dashboard/Dashboard";
        #endregion

        #region Administrative
        public const string ApproveCustomer = "Administrative/ApproveCustomer";
        public const string CreateCustomerAccount = "Administrative/CreateCustomerAccount";
        public const string RejectCustomer = "Administrative/RejectCustomer";
        public const string ApproveAccount = "Administrative/ApproveAccount";
        public const string RejectAccount = "Administrative/RejectAccount";
        public const string GetPendingTransactions = "Administrative/GetPendingTransactions";
        public const string ApproveDeposit = "Administrative/ApproveDeposit";
        public const string ApproveWithdraw = "Administrative/ApproveWithdraw";
        public const string AddEmployee = "Administrative/AddEmployee";

        #endregion

    }
}
