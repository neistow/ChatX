import { EnumMembersDescription } from '../../core/utils';

export enum Gender {
  Male = 1 << 0,
  Female = 1 << 1,
}

export namespace Gender {
  export const Flags: readonly Gender[] = [
    Gender.Male,
    Gender.Female
  ];

  export const Description: EnumMembersDescription = [
    { value: Gender.Male, label: 'Male' },
    { value: Gender.Female, label: 'Female' }
  ];
}
