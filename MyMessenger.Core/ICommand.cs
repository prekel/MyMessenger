using System.Threading.Tasks;

namespace MyMessenger.Core
{
	public interface ICommand
	{
		void Execute();
		Task ExecuteAsync();
	}
}