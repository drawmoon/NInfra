# NInfra.LinqExtensions

C# Linq extension method.

- [Sort By](#sort-by)
- [Search](#search)
- [Page By](#page-by)

## Sort-By

Support one or more sort-by expressions.

```c#
query = query.SortBy(new SortByExpression("[Name Asc]"), new SortByExpression("[CreatedTime Desc]"));
```

Use subs.

```c#
var subs = new Dictionary<string, string>
{
    { "Code", "Id" }
};

query = query.SortBy(subs, new SortByExpression("[Code Asc]"));
```

## Search

```c#

```

## Page-By

```c#
query = query.PageBy(1, 10);
```

Or

```c#
PagedList<User> users = await query.ToPagedListAsync(1, 10);
```

## License

[MIT](LICENSE)
