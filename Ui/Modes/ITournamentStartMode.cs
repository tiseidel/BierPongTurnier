using BierPongTurnier.Common;

namespace BierPongTurnier.Ui.Modes
{
    public interface ITournamentStartMode
    {
        IStartTournamentCallback StartTournamentCallback { get; set; }

        Command StartCommand { get; }
    }
}