module Domain

open Dtos
open DomainTypes
open ValidDomains
// DTO -> ValidDomains
// Going from DTO -> EmployeeCreated -> ValidDomains
let employeeCreatedToDomain (e: EmployeeCreatedDto) =
    let employeeCreated = {
        EmployeeCreated.employee = {
            id = e.employee.id
            firstname = e.employee.firstname
            lastname = e.employee.lastname
            gender = e.employee.gender
            dob = e.employee.dob
        }
        employment = {
            id = e.employment.id
            hire_date = e.employment.hire_date
            termination_date = e.employment.termination_date
            department_id = e.employment.department_id
            employee_id = e.employment.employee_id
            payroll_id = e.employment.payroll_id
        }
        payroll = {
            id = e.payroll.id
            employee_id = e.payroll.employee_id
            amount = e.payroll.amount
            cycle = e.payroll.cycle
        }
    }

    employeeCreated |> ValidDomains.EmployeeCreated

let EmployeeDeletedToDomain (e: EmployeeDeletedDto) =
    let employeeDeleted = {
        employee = {
            id = e.employee.id
            firstname = e.employee.firstname
            lastname = e.employee.lastname
            gender = e.employee.gender
            dob = e.employee.dob
        }
    }

    employeeDeleted |> EmployeeDeleted

let EmployeeUpdatedToDomain (e: EmployeeUpdatedDto) =
    let employeeUpdated = {
        employee = {
            id = e.employee.id
            firstname = e.employee.firstname
            lastname = e.employee.lastname
            gender = e.employee.gender
            dob = e.employee.dob
        }
        employment = {
            id = e.employment.id
            hire_date = e.employment.hire_date
            termination_date = e.employment.termination_date
            department_id = e.employment.department_id
            employee_id = e.employment.employee_id
            payroll_id = e.employment.payroll_id
        }
        payroll = {
            id = e.payroll.id
            employee_id = e.payroll.employee_id
            amount = e.payroll.amount
            cycle = e.payroll.cycle
        }
    }

    employeeUpdated |> EmployeeUpdated

let EmploymentCreatedToDomain (e: EmploymentCreatedDto) =
    let employmentCreated = {
        id = e.id
        hire_date = e.hire_date
        termination_date = e.termination_date
        department_id = e.department_id
        employee_id = e.employee_id
        payroll_id = e.payroll_id
    }

    employmentCreated |> EmploymentUpdated

let EmploymentDeletedToDomain (e: EmploymentDeletedDto) =
    let employmentDeleted = {
        id = e.id
        hire_date = e.hire_date
        termination_date = e.termination_date
        department_id = e.department_id
        employee_id = e.employee_id
        payroll_id = e.payroll_id
    }

    employmentDeleted |> EmploymentUpdated


let EmploymentUpdatedToDomain (e: EmploymentUpdatedDto) =
    let employmentUpdated = {
        id = e.id
        hire_date = e.hire_date
        termination_date = e.termination_date
        department_id = e.department_id
        employee_id = e.employee_id
        payroll_id = e.payroll_id
    }

    employmentUpdated |> EmploymentUpdated

let EmploymentTerminatedToDomain (e: EmploymentTerminatedDto) =
    let employmentTerminated = {
        id = e.id
        termination_date = e.termination_date
    }

    employmentTerminated |> EmploymentTerminated

let PayrollCreatedToDomain (e: PayrollCreatedDto) =
    let payrollCreated = {
        id = e.id
        employee_id = e.employee_id
        amount = e.amount
        cycle = e.cycle
    }

    payrollCreated |> PayrollUpdated

let PayrollUpdatedToDomain (e: PayrollUpdatedDto) =
    let payrollUpdated = {
        id = e.id
        employee_id = e.employee_id
        amount = e.amount
        cycle = e.cycle
    }

    payrollUpdated |> PayrollUpdated

let NewDivisionToDomain (e: NewDivisionDto) =
    let newDivision = {
        id = e.id
        name = e.name
    }

    newDivision |> DeletedDivision

let UpdatedDivisionToDomain (e: UpdatedDivisionDto) =
    let updatedDivision = {
        id = e.id
        name = e.name
    }

    updatedDivision |> DeletedDivision

let DeletedDivisionToDomain (e: DeletedDivisionDto) =
    let deletedDivision = {
        id = e.id
        name = e.name
    }

    deletedDivision |> DeletedDivision


let inline toDomain dto :ValidDomains =
    match dto with
    | EmployeeCreatedDto dto ->
        dto
        |> employeeCreatedToDomain
    | EmployeeDeletedDto dto ->
        dto
        |> EmployeeDeletedToDomain
    | EmployeeUpdatedDto dto ->
        dto
        |> EmployeeUpdatedToDomain
    | EmploymentCreatedDto dto ->
        dto
        |> EmploymentCreatedToDomain
    | EmploymentDeletedDto dto ->
        dto
        |> EmploymentDeletedToDomain
    | EmploymentUpdatedDto dto ->
        dto
        |> EmploymentUpdatedToDomain
    | EmploymentTerminatedDto dto ->
        dto
        |> EmploymentTerminatedToDomain
    | PayrollCreatedDto dto ->
        dto
        |> PayrollCreatedToDomain
    | PayrollUpdatedDto dto ->
        dto
        |> PayrollUpdatedToDomain
    | NewDivisionDto dto ->
        dto
        |> NewDivisionToDomain
    | UpdatedDivisionDto dto ->
        dto
        |> UpdatedDivisionToDomain
    | DeletedDivisionDto dto ->
        dto
        |> DeletedDivisionToDomain