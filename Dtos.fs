module Dtos

type NewDivisionDto =
  {
    id: int
    name: string
  }

type UpdatedDivisionDto =
    {
        id: int
        name: string
    }

type DeletedDivisionDto =
    {
        id: int
        name: string
    }

type EmployeeDto =
    {
        id: int
        firstname: string
        lastname: string
        dob: string
        gender: string
    }

type PayrollCreatedDto =
    {
        id: int
        employee_id: int
        amount: float
        cycle: string
    }

type PayrollUpdatedDto =
    {
        id: int
        employee_id: int
        amount: float
        cycle: string
    }

type EmploymentCreatedDto =
    {
        id: int
        hire_date: string
        termination_date: string option
        department_id: int
        employee_id: int
        payroll_id: int
    }

type EmploymentDeletedDto =
    {
        id: int
        hire_date: string
        termination_date: string option
        department_id: int
        employee_id: int
        payroll_id: int
    }

type EmploymentUpdatedDto =
    {
        id: int
        hire_date: string
        termination_date: string option
        department_id: int
        employee_id: int
        payroll_id: int
    }

type EmploymentTerminatedDto =
    {
        id: int
        termination_date: string
    }

type EmployeeDeletedDto =
    {
        employee: EmployeeDto
    }

type EmployeeCreatedDto =
    {
        employee: EmployeeDto
        employment: EmploymentCreatedDto
        payroll: PayrollCreatedDto
    }

type EmployeeUpdatedDto =
    {
        employee: EmployeeDto
        employment: EmploymentCreatedDto
        payroll: PayrollCreatedDto
    }

type Dto =
    | EmployeeCreatedDto of EmployeeCreatedDto
    | EmployeeDeletedDto of EmployeeDeletedDto
    | EmployeeUpdatedDto of EmployeeUpdatedDto
    | EmploymentCreatedDto of EmploymentCreatedDto
    | EmploymentDeletedDto of EmploymentDeletedDto
    | EmploymentUpdatedDto of EmploymentUpdatedDto
    | EmploymentTerminatedDto of EmploymentTerminatedDto
    | PayrollCreatedDto of PayrollCreatedDto
    | PayrollUpdatedDto of PayrollUpdatedDto
    | NewDivisionDto of NewDivisionDto
    | UpdatedDivisionDto of UpdatedDivisionDto
    | DeletedDivisionDto of DeletedDivisionDto
