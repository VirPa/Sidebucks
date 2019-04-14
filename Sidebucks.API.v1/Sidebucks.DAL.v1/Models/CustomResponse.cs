using System.Collections.Generic;

namespace Sidebucks.DAL.v1.Models {
    public class CustomResponse<T> {

        public CustomResponse() {
            Message = new List<string>();
        }

        public bool Succeed { get; set; }

        public T Data { get; set; }

        public List<string> Message { get; set; }
    }
}
