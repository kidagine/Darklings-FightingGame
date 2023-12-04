public class TrainingSettings
{
	public static bool ShowHitboxes = false;
	public static BlockType Block = BlockType.Off;
	public static int BlockCount = 0;
	public static int BlockCountCurrent = 0;
	public static bool OnHit = false;
	public static bool CpuOff = false;
	public static int Stance = 0;

	public enum BlockType { Off, BlockAlways, BlockCount }
}
