import { EnumMembersDescription } from '../../core/utils';

export enum AgeRange {
  BelowSeventeen = 1 << 0,
  EighteenToTwentyFour = 1 << 1,
  TwentyFiveToThirteen = 1 << 2,
  AboveThirty = 1 << 3
}

export namespace AgeRange {
  export const Flags: readonly AgeRange[] = [
    AgeRange.BelowSeventeen,
    AgeRange.EighteenToTwentyFour,
    AgeRange.TwentyFiveToThirteen,
    AgeRange.AboveThirty
  ];

  export const Description: EnumMembersDescription = [
    { value: AgeRange.BelowSeventeen, label: 'Below 17' },
    { value: AgeRange.EighteenToTwentyFour, label: '18 to 24' },
    { value: AgeRange.TwentyFiveToThirteen, label: '25 to 30' },
    { value: AgeRange.AboveThirty, label: 'Above 30' }
  ];
}
