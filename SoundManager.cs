using _06_tankWar.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;



namespace _06_tankWar
{
    class SoundManager
    {
        private static readonly SoundPlayer startPlayer = new SoundPlayer();
        private static readonly SoundPlayer addPlayer = new SoundPlayer();
        private static readonly SoundPlayer blastPlayer = new SoundPlayer();
        private static readonly SoundPlayer firePlayer = new SoundPlayer();
        private static readonly SoundPlayer hitPlayer = new SoundPlayer();

        public static void InitSound()
        {
            startPlayer.Stream = Resources.start;
            addPlayer.Stream = Resources.add;
            blastPlayer.Stream = Resources.blast;
            firePlayer.Stream = Resources.fire;
            hitPlayer.Stream = Resources.hit;
        }

        public static Task PlayStartAsync()
        {
            return Task.Run(() => startPlayer.Play());
        }

        public static Task PlayAddAsync()
        {
            return Task.Run(() => addPlayer.Play());
        }

        public static Task PlayBlastAsync()
        {
            return Task.Run(() => blastPlayer.Play());
        }

        public static Task PlayFireAsync()
        {
            return Task.Run(() => firePlayer.Play());
        }

        public static Task PlayHitAsync()
        {
            return Task.Run(() => hitPlayer.Play());
        }
    }
}
