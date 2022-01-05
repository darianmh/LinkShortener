namespace LinkShortener.ViewModel.Api
{
    public class JsonResult<T>
    {
        public bool Ok { get; set; }
        public string Description { get; set; }
        public T Data { get; set; }

        /// <summary>
        /// اگر خطا وجود دارد.، توضیحات متن خطا است
        /// </summary>
        public JsonResult(string description)
        {
            Ok = false;
            Description = description;
        }
        /// <summary>
        /// اگر همه چیز درست باشد.
        /// </summary>
        /// <param name="data"></param>
        public JsonResult(T data)
        {
            Data = data;
            Ok = true;
        }

        public JsonResult(bool ok, T data, string description)
        {
            Data = data;
            Description = description;
            Ok = ok;
        }
    }
}
