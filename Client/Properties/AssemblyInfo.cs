using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// 有关程序集的常规信息通过以下
// 特性集控制。更改这些特性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle("SafeIM")]
[assembly: AssemblyDescription(@"  安全及时通信程序，类似与QQ，分为客户端和服务器两部分。
  服务器数据库不保存用户明文密码，只保存密码哈希值，采用公钥密码和对称密码体制结合的形式，对所有传输过程进行加密。为了防止消息重放攻击，采用挑战应答方式登陆验证。
  可以发送文本消息，能设置不同字体，发送表情，窗口抖动，截图，传送文件，语音视频（网络状况较好70K/s-80K/s,视频压缩率比起H.264较低），所有文本消息，信息，文件，视频语音的发送，无论是客户端与服务器间，客户端与客户端之间均加密传输。
  本程序引用了部分互联网资源，类库，源码，请勿用于商用。")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("中国科学院大学 13计控 董颖")]
[assembly: AssemblyProduct("SafeIM")]
[assembly: AssemblyCopyright("Copyright © pinkee 2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// 将 ComVisible 设置为 false 使此程序集中的类型
// 对 COM 组件不可见。如果需要从 COM 访问此程序集中的类型，
// 则将该类型上的 ComVisible 特性设置为 true。
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("445e438b-ac95-42ff-8fd4-0398fa5950a8")]

// 程序集的版本信息由下面四个值组成:
//
//      主版本
//      次版本 
//      生成号
//      修订号
//
// 可以指定所有这些值，也可以使用“生成号”和“修订号”的默认值，
// 方法是按如下所示使用“*”:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
