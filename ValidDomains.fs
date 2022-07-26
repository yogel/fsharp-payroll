module ValidDomains

open DomainTypes


type ValidDomains =
    | EmployeeCreated of EmployeeCreated
    | EmployeeDeleted of EmployeeDeleted
    | EmployeeUpdated of EmployeeUpdated
    | EmploymentCreated of EmploymentCreated
    | EmploymentDeleted of EmploymentDeleted
    | EmploymentUpdated of EmploymentUpdated
    | EmploymentTerminated of EmploymentTerminated
    | PayrollCreated of PayrollCreated
    | PayrollUpdated of PayrollUpdated
    | NewDivision of NewDivision
    | DeletedDivision of DeletedDivision