import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'summary'
})
export class SummaryPipe implements PipeTransform {

  transform(content :string, charLimit :number): string {

    if(content.length<= charLimit)
    {
      return content;
    }
    return `${content.substring(0,charLimit)} ...`
    
  }

}
