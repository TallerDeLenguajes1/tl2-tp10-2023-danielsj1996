namespace tl2_tp10_2023_danielsj1996.ViewModels;

    public class ListarUsuariosViewModel
    {
        public List<UsuarioViewModel> ListaUsuariosVM { get; set; }
        public ListarUsuariosViewModel(List<UsuarioViewModel> usuariosVM)
        {
            ListaUsuariosVM = usuariosVM;
        }
        public ListarUsuariosViewModel()
        {
            ListaUsuariosVM = new List<UsuarioViewModel>();
        }
    }

