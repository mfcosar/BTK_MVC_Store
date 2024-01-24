using System.ComponentModel.DataAnnotations;

namespace StoreApp.Models
{
    public class LoginModel
    { // Servis katmanında bir işlem yapmadığımız için doğrudan StoreApp altında /Models/ altında tanımlıyoruz

        private string? _returnurl;

        [Required(ErrorMessage ="Name is required.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }

        public string ReturnUrl
        {
            get
            {
                if (_returnurl is null)
                    return "/";
                else return _returnurl;
            }
            set
            {
                _returnurl = value;
            }
        }
    }
}
