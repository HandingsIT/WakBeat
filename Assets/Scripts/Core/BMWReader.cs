using System;
using System.Collections.Generic;
using UnityEngine;


public class MusicInfoItem
{
    public string Title;    // ���
    public string Artist;   // �۰
    public float BPM;         // BPM
    public int Bar;         // Bar (�Ѹ��� == ��ĭ)
    public int Time;        // ���� �ð�

    public MusicInfoItem(string title, string artist, float bpm, int bar, int time)
    {
        Title = title;
        Artist = artist;
        BPM = bpm;
        Bar = bar;
        Time = time;
    }

}

public class AnimationItem
{
    public int Index;
    public string StageName;
    public string AnimationName;

    public AnimationItem(int index, string stageName, string animationName)
    {
        Index = index;
        StageName = stageName;
        AnimationName = animationName;
    }

}

public class DodgePoint
{
    public int Index = -1;

    public DodgePoint(int index)
    {
        Index = index;
    }
}

public class OutObstacle
{
    public int Index = -1;

    public OutObstacle(int index)
    {
        Index = index;
    }
}

public class InObstacle
{
    public int Index = -1;

    public InObstacle(int index)
    {
        Index = index;
    }
}

public class DummyDodgePoint
{
    public int Index = -1;

    public DummyDodgePoint(int index)
    {
        Index = index;
    }
}

public class DummyOutObstacle
{
    public int Index = -1;

    public DummyOutObstacle(int index)
    {
        Index = index;
    }
}

public class DummyInObstacle
{
    public int Index = -1;

    public DummyInObstacle(int index)
    {
        Index = index;
    }
}

public class ChartingItem
{
    public int Beat;                     // ä�����α׷��� ����(4/4���̸� 4���� ����)�� ���� 
    public int AnimationIndex;           // �ִϸ��̼��� �ε���
    public float Bar;                    // ���ٿ� ���� ĭ
    public string Interval;              // �ð����� ���� ����(Delay)
    public float BallAngle;              // ������ ���� ������ ����
    public float BallAngleTime;          // ������ ���� �ð�
    public float Speed;                  // Beat�� ���� � �̵��ϴ���
    public float SpeedTime;              // �ٲ� Speed�� �����ϴ� �ð�
    public string[] DodgePoints;         // ���� ������ġ
    public string[] DummyDodgePoints;    // ���� �ٹҿ� ������ġ
    public string[] OutObstacles;        // �ٱ��� ���� ������ġ
    public string[] DummyOutObstacles;   // �ٱ��� �ٹҿ� ���� ������ġ
    public string[] InObstacles;         // ���� ���� ���� ��ġ
    public string[] DummyInObstacles;    // ���� �ٹҿ� ���� ���� ��ġ
    public int SavePoint;                // ���̺� ����Ʈ

    // For Debuging
    public string DodgePoint;
    public string OutObstacle;
    public string InObstacle;

    public List<DodgePoint> DodgePointElements = new List<DodgePoint>();                    // ���� ������ġ
    public List<OutObstacle> OutObstacleElements = new List<OutObstacle>();                 // �ٱ��� ���� ������ġ
    public List<InObstacle> InObstacleElements = new List<InObstacle>();                    // ���� ���� ���� ��ġ

    public List<DummyDodgePoint> DummyDodgePointElements = new List<DummyDodgePoint>();     // ���� ���� ������ġ
    public List<DummyOutObstacle> DummyOutObstacleElements = new List<DummyOutObstacle>();  // �ٱ��� ���� ������ġ
    public List<DummyInObstacle> DummyInObstacleElements = new List<DummyInObstacle>();     // ���� ���� ���� ��ġ

    public ChartingItem(int beat, int animationIndex, float bar, string interval, float ballAngle, float ballTime, float speed, float speedTime, string dodgePoint, string dummyDodge, string outObstacle, string dummyOut, string inObstacle, string dummyIn, int savePoint)
    {
        // Beat
        Beat = beat;

        //AnimationIndex
        AnimationIndex = animationIndex;

        // ĭ�� ��
        Bar = bar;

        //Interval
        Interval = interval;

        // Ball Angle
        BallAngle = ballAngle;
        BallAngleTime = ballTime;

        // Speed
        Speed = speed;
        SpeedTime = speedTime;

        //Dodge Point Elements
        DodgePoint = dodgePoint; // For Debuging

        string[] dodgePoints = dodgePoint.Trim().Split('|');
        DodgePoints = dodgePoints;

        for (int i = 0; i < dodgePoints.Length; i++)
        {
            int.TryParse(dodgePoints[i], out int idx);

            DodgePointElements.Add(new DodgePoint(idx));
        }

        // Dummy Dodge Point Elements
        string[] dummyDodges = dummyDodge.Trim().Split('|');
        DummyDodgePoints = dummyDodges;

        for (int i = 0; i < dummyDodges.Length; i++)
        {
            int.TryParse(dummyDodges[i], out int idx);

            DummyDodgePointElements.Add(new DummyDodgePoint(idx));
        }

        // Out Obstacle Elements
        OutObstacle = outObstacle; // For Debuging

        string[] outObstacles = outObstacle.Trim().Split('|');
        OutObstacles = outObstacles;

        for (int i = 0; i < outObstacles.Length; i++)
        {
            int.TryParse(outObstacles[i], out int idx);

            OutObstacleElements.Add(new OutObstacle(idx));
        }

        // Dummy Out Obstacle Elements
        string[] dummyOuts = dummyOut.Trim().Split('|');
        DummyOutObstacles = dummyOuts;

        for (int i = 0; i < dummyOuts.Length; i++)
        {
            int.TryParse(dummyOuts[i], out int idx);

            DummyOutObstacleElements.Add(new DummyOutObstacle(idx));
        }

        // In Obstacle Elements
        InObstacle = inObstacle; // For Debuging

        string[] inObstacles = inObstacle.Trim().Split('|');
        InObstacles = inObstacles;

        for (int i = 0; i < inObstacles.Length; i++)
        {
            int.TryParse(inObstacles[i], out int idx);

            InObstacleElements.Add(new InObstacle(idx));
        }

        // Dummy In Obstacle Elements
        string[] dummyIns = dummyIn.Trim().Split('|');
        DummyOutObstacles = dummyIns;

        for (int i = 0; i < dummyIns.Length; i++)
        {
            int.TryParse(dummyIns[i], out int idx);

            DummyInObstacleElements.Add(new DummyInObstacle(idx));
        }

        SavePoint = savePoint;
    }
}

public class ChartingList : List<ChartingItem>
{

}

public class BMWReader : CsvReader
{
    private MusicInfoItem _musicInfoItem = null;
    private List<AnimationItem> _animationItem = new List<AnimationItem>();
    private List<ChartingItem> _chartingItem = new List<ChartingItem>();

    public MusicInfoItem MusicInfoItem { get { return _musicInfoItem; } }
    public List<AnimationItem> AnimationItem { get { return _animationItem; } }
    public List<ChartingItem> ChartingItem { get { return _chartingItem; } }

    enum CSVBLOCK { Unkown, MusicInfo, Animation, Charting }
    CSVBLOCK parseBlock = CSVBLOCK.Unkown;

    protected override void Init()
    {
        base.Init();
    }

    protected override void GetItem(string line, int lineNumber)
    {
        string[] args = null;

        line = line.Trim();

        try
        {
            if (line[0] == '#')
            {
                if (line.Contains("#MUSICINFO"))
                {
                    parseBlock = CSVBLOCK.MusicInfo;
                }
                else if (line.Contains("#ANIMATION"))
                {
                    parseBlock = CSVBLOCK.Animation;
                }
                else if (line.Contains("#CHARTING"))
                {
                    parseBlock = CSVBLOCK.Charting;
                }
            }
            else
            {
                //parsed element
                args = line.Split(',');
                int count = 0;

                switch (parseBlock)
                {
                    case CSVBLOCK.MusicInfo:
                        count = 0;

                        if (args.Length > 4)
                        {
                            _musicInfoItem = new MusicInfoItem(
                                args[count++].Trim(),
                                args[count++].Trim(),
                                float.Parse(args[count++].Trim()),
                                Convert.ToInt32(args[count++].Trim()),
                                Convert.ToInt32(args[count++].Trim())
                                );

                            // �۰ �� ^ ���� ,�� ����
                            if (_musicInfoItem.Artist.Contains("^"))
                            {
                                _musicInfoItem.Artist = _musicInfoItem.Artist.Replace("^", ",");
                            }

                            Debug.Log($"{_musicInfoItem.Title},{_musicInfoItem.Artist},{_musicInfoItem.BPM},{_musicInfoItem.Time}");
                        }
                        else
                        {
                            Debug.Log("Music Info Arguments Index Error");
                        }
                        break;
                    case CSVBLOCK.Animation:
                        count = 0;
                        try
                        {
                            var itemAnimation = new AnimationItem(
                                Convert.ToInt32(args[count++].Trim()),        // ä�����α׷��� ����(4/4���̸� 4���� ����)�� ����
                                args[count++].Trim(),                         // ���� ������ġ
                                args[count++].Trim()                          // �ٱ��� ���� ������ġ
                                );

                            _animationItem.Add(itemAnimation);

                            //Debug.Log($"Anim Items : {itemAnimation.Index},{itemAnimation.StageName},{itemAnimation.AnimationName}");
                        }
                        catch (Exception e)
                        {
                            Debug.unityLogger.LogException(e);
                            Debug.LogError($"Animation BMWReader :" + line + ",line number" + lineNumber);
                        }
                        break;
                    case CSVBLOCK.Charting:
                        count = 0;
                        try
                        {
                            var itemCharting = new ChartingItem(
                                Convert.ToInt32(args[count++].Trim()),        // ä�����α׷��� ����(4/4���̸� 4���� ����)�� ����
                                Convert.ToInt32(args[count++].Trim()),        // ����� �ִϸ��̼��� �ε���
                                float.Parse(args[count++].Trim()),            // ĭ�� ��
                                args[count++].Trim(),                         // ���ڿ��� �����ϱ����� Interval // Change float to String
                                float.Parse(args[count++].Trim()),            // ������ ���� ������ ����
                                float.Parse(args[count++].Trim()),            // ����� �������� ���� �ϴ� �ð�
                                float.Parse(args[count++].Trim()),            // Beat�� ���� � �̵��ϴ���
                                float.Parse(args[count++].Trim()),            // �ٲ� Speed�� �����ϴ� �ð�
                                args[count++].Trim(),                         // ���� ������ġ
                                args[count++].Trim(),                         // ���� ���� ������ġ
                                args[count++].Trim(),                         // �ٱ��� ���� ������ġ
                                args[count++].Trim(),                         // �ٱ��� ���� ���� ������ġ
                                args[count++].Trim(),                         // ���� ���� ���� ��ġ
                                args[count++].Trim(),                         // ���� ���� ���� ���� ��ġ
                                Convert.ToInt32(args[count++].Trim())         // ���̺� ����Ʈ
                                );

                            _chartingItem.Add(itemCharting);

                            //Debug.Log($"line : {itemCharting.Beat},{itemCharting.AnimationIndex},{itemCharting.Bar},{itemCharting.Interval},{itemCharting.BallAngle},{itemCharting.BallAngleTime},{itemCharting.Speed},{itemCharting.SpeedTime},{itemCharting.DodgePoint},{itemCharting.OutObstacle},{itemCharting.InObstacle},{itemCharting.SavePoint}" );                            
                            //for (int i = 0; i < itemCharting.DodgePointElements.Count; i++)
                            //{
                            //    Debug.Log($"{itemCharting.DodgePointElements[i].Index}");
                            //}
                        }
                        catch (Exception e)
                        {
                            Debug.unityLogger.LogException(e);
                            Debug.LogError($"Charting BMWReader :" + line + ",line number" + lineNumber);
                        }
                        break;
                    default:
                        break;
                }
            }

        }
        catch(Exception e)
        {
            Debug.unityLogger.LogException(e);
            Debug.LogError($"Exception : {FileName} : {lineNumber} : {line}");
        }
    }
}
