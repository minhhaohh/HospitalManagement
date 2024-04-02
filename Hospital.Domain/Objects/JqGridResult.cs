namespace Hospital.Domain.Objects
{
    public class JqGridResult<T>
    {
        public int total {  get; set; }

        public int page { get; set; }

        public int records { get; set; }

        public List<T> rows { get; set; }

        public JqGridResult() 
        { 
            total = 0;
            page = 0;
            records = 0;
            rows = new List<T>();
        }

        public JqGridResult(int total, int page, int records, List<T> rows)
        {
            this.total = total;
            this.page = page;
            this.records = records;
            this.rows = rows;
        }
    }
}
