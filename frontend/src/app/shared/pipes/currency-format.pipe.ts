import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'currencyFormat', standalone: true })
export class CurrencyFormatPipe implements PipeTransform {
  transform(
    value: number | null | undefined,
    currencyCode: string = 'USD',
    symbol: string = '$'
  ): string {
    if (value == null || isNaN(value)) return `${symbol}0.00`;
    return `${symbol}${value.toLocaleString('en-US', {
      minimumFractionDigits: 2,
      maximumFractionDigits: 2,
    })}`;
  }
}
