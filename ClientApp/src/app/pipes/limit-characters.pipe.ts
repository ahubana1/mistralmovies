import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'limitCharacters'
})
export class LimitCharactersPipe implements PipeTransform {

  transform(intro: string, limit:number): string {
    if(intro.length < limit) return intro;
    return intro.slice(0,limit-3) + "..."; 
  }

}
 