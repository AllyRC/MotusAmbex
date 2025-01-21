export default interface ApiResponse<TData>
{
  success?: boolean,
  message?: string,
  data?: TData | undefined,
  totalRecords? : number
}
