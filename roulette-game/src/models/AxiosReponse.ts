import { Result } from "./Result";

export interface AxiosResponse<T> {
  data: Result<T>;
}
