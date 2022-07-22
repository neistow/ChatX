import { Gender } from './gender.enum';
import { AgeRange } from './age-range.enum';

export interface UserInfo {
  gender: Gender;
  age: AgeRange;
}
