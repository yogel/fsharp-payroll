module DomainTypes

type NewDivision =
    {
        id: int
        name: string
    }

type DeletedDivision =
    {
        id: int
        name: string
    }

type Employee =
    {
        id: int
        firstname: string
        lastname: string
        dob: string
        gender: string
    }

type PayrollCreated =
    {
        id: int
        employee_id: int
        amount: float
        cycle: string
    }

type PayrollUpdated =
    {
        id: int
        employee_id: int
        amount: float
        cycle: string
    }

type EmploymentCreated =
    {
        id: int
        hire_date: string
        termination_date: string option
        department_id: int
        employee_id: int
        payroll_id: int
    }

type EmploymentDeleted =
    {
        id: int
        hire_date: string
        termination_date: string option
        department_id: int
        employee_id: int
        payroll_id: int
    }

type EmploymentUpdated =
    {
        id: int
        hire_date: string
        termination_date: string option
        department_id: int
        employee_id: int
        payroll_id: int
    }

type EmploymentTerminated =
    {
        id: int
        termination_date: string
    }

type EmployeeDeleted =
    {
        employee: Employee
    }

type EmployeeCreated =
    {
        employee: Employee
        employment: EmploymentCreated
        payroll: PayrollCreated
    }

type EmployeeUpdated =
    {
        employee: Employee
        employment: EmploymentCreated
        payroll: PayrollCreated
    }