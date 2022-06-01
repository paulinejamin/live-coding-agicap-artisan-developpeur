namespace LiveCoding.Services;

internal record BarNotFound() : Bar(new BarName(string.Empty), 0, Array.Empty<DayOfWeek>(), false);