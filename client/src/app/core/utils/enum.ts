export type EnumMembersDescription = readonly { value: number, label: string }[];

export function combineEnumFlags(values: readonly number[]): number {
  return values.reduce((prev: number, curr: number) => prev | curr, 0);
}

export function splitEnumFlags(value: number, enumFlags: readonly number[]): number[] {
  return enumFlags.reduce((prev, curr) => {
    if (value & curr) {
      prev.push(curr);
    }
    return prev;
  }, [] as number[]);
}
