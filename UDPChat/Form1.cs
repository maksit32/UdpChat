using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace UDPChat
{
    public partial class Form1 : Form
    {
        private UdpClient client = new UdpClient();
        //private IPAddress ip = IPAddress.Parse("192.168.0.12");
        //private string ip = "192.168.0.12";
        private int port = 4372;

        public Form1()
        {
            InitializeComponent();

            //����������� ��������� ������� �� ���������� ������������� ������ ����� � ���������
            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            client.ExclusiveAddressUse = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //����� ����� ���������
            ReceiveMessages();
        }

        //�������� � ����� ���
        private void sendButton_Click(object sender, EventArgs e)
        {
            //��� ����� ���������
            byte[] bytes = Encoding.Unicode.GetBytes(this.mesReceiveRichTextBox1.Text);
            client.Send(bytes, bytes.Length, "255.255.255.255", port);//���� ���������   �����������������
        }
        //���������� ����� ���������
        private async void ReceiveMessages()
        {
            //�����������
            client.Client.Bind(new IPEndPoint(IPAddress.Any, port));
            while (true)
            {
                UdpReceiveResult result = await client.ReceiveAsync();
                //������������
                this.messagesRichTextBox.Text += Encoding.Unicode.GetString(result.Buffer) + "\n";
            }
        }
    }
}