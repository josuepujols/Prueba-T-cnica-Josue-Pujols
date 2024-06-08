export interface Result<T> {
  message: string;
  success: boolean;
  statusCode: number;

  data: T;
}
