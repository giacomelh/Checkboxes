namespace Checkboxes
{
    public partial class MainPage : ContentPage
    {
        // guarda os checkboxes criados pra fazer a contagem
        List<CheckBox> listaChecks = new List<CheckBox>();

        public MainPage()
        {
            InitializeComponent();
            AtualizarContador();
        }

        private void btnAdicionar_Clicked(object? sender, EventArgs e)
        {
            string tarefa = txtTarefa.Text?.Trim() ?? "";

            // nao deixa adicionar tarefa vazia
            if (tarefa == "")
            {
                txtTarefa.Focus();
                return;
            }

            CheckBox chk = new CheckBox();
            chk.VerticalOptions = LayoutOptions.Center;
            chk.CheckedChanged += chkTarefa_CheckedChanged;

            Label lb = new Label();
            lb.Text = tarefa;
            lb.FontSize = 16;
            lb.VerticalOptions = LayoutOptions.Center;

            HorizontalStackLayout linha = new HorizontalStackLayout();
            linha.Spacing = 5;
            linha.Children.Add(chk);
            linha.Children.Add(lb);

            stkTarefas.Children.Add(linha);
            listaChecks.Add(chk);

            txtTarefa.Text = "";
            AtualizarContador();
        }

        private void chkTarefa_CheckedChanged(object? sender, CheckedChangedEventArgs e)
        {
            // a label fica do lado do checkbox, no mesmo HorizontalStackLayout
            if (sender is not CheckBox chk || chk.Parent is not HorizontalStackLayout linha)
                return;

            Label lb = (Label)linha.Children[1];

            if (e.Value)
            {
                lb.TextDecorations = TextDecorations.Strikethrough;
                lb.TextColor = Colors.Gray;
            }
            else
            {
                lb.TextDecorations = TextDecorations.None;
                // tira a cor setada e volta pra cor padrao do Styles.xaml
                lb.ClearValue(Label.TextColorProperty);
            }

            AtualizarContador();
        }

        private void AtualizarContador()
        {
            int concluidas = 0;

            foreach (CheckBox chk in listaChecks)
            {
                if (chk.IsChecked)
                    concluidas++;
            }

            lbContador.Text = $"{concluidas} de {listaChecks.Count} tarefas concluídas";
        }
    }
}
