import axios, { AxiosInstance, AxiosResponse } from "axios";
import { Roulette } from "../models/roulette";
import { Player } from "../models/player";
import { BetResult } from "../models/BetResult";
import { Result } from "../models/Result";

export class RouletteService {
  private http: AxiosInstance;

  constructor() {
    this.http = axios.create({
      baseURL: "http://localhost:4050/api/roulette",
      headers: {
        "Content-Type": "application/json",
      },
    });
  }

  public async getUserAmount(name: string): Promise<Player | null> {
    var promise = this.http.get<any, AxiosResponse<Result<Player>>>(`/player/${name}`);
    const response = await promise;
    const result = response.data;
    if (result.success) {
      return result.data;
    }
    return null;
  }

  public async saveBet(name: string, amount: number): Promise<boolean> {
    const promise = this.http.post<any, AxiosResponse<Result<Player>>>(`/player/addOrUpdate`, { name: name, amount: amount });
    const response = await promise;
    const result = response.data;
    if (result.success) {
      return true;
    }
    return false;
  }

  public async getNumberAndColor(): Promise<Roulette | null> {
    var promise = this.http.get<any, AxiosResponse<Result<Roulette>>>(`/openGame`);
    const response = await promise;
    const result = response.data;
    if (result.success) {
      return result.data;
    }
    return null;
  }

  public async placeBetByColor(
    userColor: string,
    rouletteColor: string,
    bet: number,
    amount: number
  ): Promise<BetResult | null> {
    const promise = this.http.post<any, AxiosResponse<Result<BetResult>>>(`/betColor`, {
      amount: amount,
      bet: bet,
      colorSelected: userColor,
      colorRoulette: rouletteColor,
    });
    const response = await promise;
    const result = response.data;
    if (result.success) {
      return result.data;
    }
    return null;
  }

  public async placeBetByNumber(
    userNumber: string,
    rouletteNumber: number,
    userColor: string,
    rouletteColor: string,
    bet: number,
    amount: number
  ): Promise<BetResult | null> {
    const promise = this.http.post<any, AxiosResponse<Result<BetResult>>>(`/betNumberAndColor`, {
      amount: amount,
      bet: bet,
      colorSelected: userColor,
      colorRoulette: rouletteColor,
      numberSelected: userNumber,
      numberRoulette: rouletteNumber,
    });
    const response = await promise;
    const result = response.data;
    if (result.success) {
      return result.data;
    }
    return null;
  }

  public async placeBetByEvenOrOdd(
    isOdd: boolean,
    rouletteNumber: number,
    userColor: string,
    rouletteColor: string,
    bet: number,
    amount: number
  ): Promise<BetResult | null> {
    const promise = this.http.post<any, AxiosResponse<Result<BetResult>>>(`/betOddOrEven`, {
      amount: amount,
      bet: bet,
      colorSelected: userColor,
      colorRoulette: rouletteColor,
      numberRoulette: rouletteNumber,
      isOdd: isOdd,
    });
    const response = await promise;
    const result = response.data;
    if (result.success) {
      return result.data;
    }
    return null;
  }

}
