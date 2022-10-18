export class EmployeePagedList {
    page?:number;
    pageSize?:number;
    totalCount?:number;
    items:IManagers[];

}

export interface IManagers
{
    id:string;
    managerName:string;
    email:string;
    team:IAllEmployees[]
}
export interface IAllEmployees
{
      id:string
      employeeName:string
      email:string
}