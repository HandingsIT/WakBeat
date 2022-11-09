using System;
using System.Collections.Generic;
using UnityEngine;

public class MusicInfoItem
{
    public string Title;
    public string Artist;
    public int BPM;
    public int Time;

    public MusicInfoItem(string title, string artist, int bpm, int time)
    {
        Title = title;
        Artist = artist;
        BPM = bpm;
        Time = time;
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

public class ChartingItem
{
    public int Beat;                     // ä�����α׷��� ����(4/4���̸� 4���� ����)�� ���� 
    public string DodgePoint;            // ���� ������ġ
    public string OutObstacle;           // �ٱ��� ���� ������ġ
    public string InObstacle;            // ���� ���� ���� ��ġ
    public int SavePoint;                // ���̺� ����Ʈ
    public float Speed;                  // Beat�� ���� � �̵��ϴ���
    public float SpeedTime;              // �ٲ� Speed�� �����ϴ� �ð�
    public float BallAngle;              // ������ ���� ������ ����
    public int IsKnockback;              // �ӹ� ��� Ȱ��ȭ
    public float KnockBackAngle;         // �ӹ� ��� Ȱ��ȭ�� �ӹ� ����

    public List<DodgePoint> DodgePointElements = new List<DodgePoint>();        // ���� ������ġ
    public List<OutObstacle> OutObstacleElements = new List<OutObstacle>();     // �ٱ��� ���� ������ġ
    public List<InObstacle> InObstacleElements = new List<InObstacle>();        // ���� ���� ���� ��ġ

    public ChartingItem(int beat, string correctBeat, string outObstacle, string inObstacle, int savePoint, float speed, float speedTime, float ballAngle, int isKnockBack, float knockBackAngle)
    {
        // Beat
        Beat = beat;
        DodgePoint = correctBeat;
        OutObstacle = outObstacle;
        InObstacle = inObstacle;

        // Dodge Point Elements
        string[] correctBeats = correctBeat.Split('|');

        for (int i = 0; i < correctBeats.Length; i++)
        {
            int idx = Convert.ToInt32(correctBeats[i]);
            DodgePointElements.Add(new DodgePoint(idx));
        }

        // Out Obstacle Elements
        string[] outObstacles = outObstacle.Split('|');

        for (int i = 0; i < outObstacles.Length; i++)
        {
            int idx = Convert.ToInt32(outObstacles[i]);
            OutObstacleElements.Add(new OutObstacle(idx));
        }

        // In Obstacle Elements
        string[] inObstacles = inObstacle.Split('|');

        for (int i = 0; i < inObstacles.Length; i++)
        {
            int idx = Convert.ToInt32(inObstacles[i]);
            InObstacleElements.Add(new InObstacle(idx));
        }

        SavePoint = savePoint;
        Speed = speed;
        SpeedTime = speedTime;
        BallAngle = ballAngle;
        IsKnockback = isKnockBack;
        KnockBackAngle = knockBackAngle;
    }
}

public class ChartingList : List<ChartingItem>
{

}

public class BMWReader : CsvReader
{
    private MusicInfoItem _musicInfoItem;
    private List<ChartingItem> _chartingItem = new List<ChartingItem>();

    public MusicInfoItem MusicInfoItem { get { return _musicInfoItem; } }
    public List<ChartingItem> ChartingItem { get { return _chartingItem; } }

    enum CSVBLOCK { Unkown, MusicInfo, Charting }
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

                        _musicInfoItem = new MusicInfoItem(
                            args[count++].Trim(),
                            args[count++].Trim(),
                            Convert.ToInt32(args[count++].Trim()),
                            Convert.ToInt32(args[count++].Trim())                            
                            );

                        // �۰ �� ^ ���� ,�� ����
                        if (_musicInfoItem.Artist.Contains("^"))
                        {
                            _musicInfoItem.Artist = _musicInfoItem.Artist.Replace("^", ",");
                        }

                        Debug.Log($"{_musicInfoItem.Title},{_musicInfoItem.Artist},{_musicInfoItem.BPM},{_musicInfoItem.Time}");
                        break;
                    case CSVBLOCK.Charting:
                        count = 0;
                        try
                        {
                            var itemCharting = new ChartingItem(
                                Convert.ToInt32(args[count++].Trim()),        // ä�����α׷��� ����(4/4���̸� 4���� ����)�� ����
                                args[count++].Trim(),                         // ���� ������ġ
                                args[count++].Trim(),                         // �ٱ��� ���� ������ġ
                                args[count++].Trim(),                         // ���� ���� ���� ��ġ
                                Convert.ToInt32(args[count++].Trim()),        // ���̺� ����Ʈ
                                float.Parse(args[count++].Trim()),            // Beat�� ���� � �̵��ϴ���
                                float.Parse(args[count++].Trim()),            // �ٲ� Speed�� �����ϴ� �ð�
                                float.Parse(args[count++].Trim()),            // ������ ���� ������ ����
                                Convert.ToInt32(args[count++].Trim()),        // �ӹ� ��� Ȱ��ȭ
                                float.Parse(args[count++].Trim())             // �ӹ� ��� Ȱ��ȭ�� �ӹ� ����
                                );

                            _chartingItem.Add(itemCharting);
                            Debug.Log($"line : {itemCharting.Beat},{itemCharting.DodgePoint},{itemCharting.OutObstacle},{itemCharting.InObstacle},{itemCharting.SavePoint},{itemCharting.Speed},{itemCharting.SpeedTime},{itemCharting.BallAngle},{itemCharting.IsKnockback},{itemCharting.KnockBackAngle}" );
                        }
                        catch (Exception e)
                        {
                            Debug.unityLogger.LogException(e);
                            Debug.LogError("Charting BMSReader :" + line + ",line number" + lineNumber);
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
