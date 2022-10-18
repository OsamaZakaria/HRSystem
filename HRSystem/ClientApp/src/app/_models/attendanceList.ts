export class AttendancePagedList {
    page?:number;
    pageSize?:number;
    totalCount?:number;
    items:AttendanceList[];

}
export class AttendanceList
{
    employeeId:string;
    name:string;
    logDate:Date;
    totalWorkingHours:any;
    logDetails:AttendeneLogDetails[];
    hasViolation:boolean
}
export interface AttendeneLogDetails
{
    timeIn:any;
    timeTimeOut:any;
}