namespace MyGameList.Src.Shared.Utils
{
    public class Util
    {
        public static async Task AddCollectionProperties<T>(
            ICollection<T> collection,
            List<int> ids,
            Func<List<int>, Task<List<T>>> getListByIdsFunc)
        {
            var idsToAdd = ids
                .Except(collection
                    .Select(item => (int)typeof(T)
                        .GetProperty("Id")?
                        .GetValue(item)!))
                .ToList();

            var itemsToAdd = await getListByIdsFunc(idsToAdd);

            foreach (var item in itemsToAdd)
            {
                collection.Add(item);
            }
        }
    }
}
