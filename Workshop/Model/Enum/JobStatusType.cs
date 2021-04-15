namespace Workshop.Model.Enum
{
    public  enum JobStatusType
    {
       Unspecified, //未指定
	   Running, //执行中
	   Rerunning, //守护重新执行中
	   Stop,//停止中
	   Obsolete, //已结束
       Pending  //挂起中
    }
}
