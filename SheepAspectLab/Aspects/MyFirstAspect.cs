using System;
using System.Diagnostics;
using SheepAspect.Framework;
using SheepAspect.Runtime;
using System.Windows.Forms;

namespace SheepAspectLab.Aspects
{
    /// <summary>
    /// 謹做為weaving使用。
    /// </summary>
    public class CatchAndLogAttribute : Attribute
    { }

    /// <summary>
    /// 謹做為weaving使用。
    /// </summary>
    public class WaitCursorAttribute : Attribute
    { }

    [Aspect]
    public class MyFirstAspect
    {
        /// 
        /// 雖 SheepAspect 很有彈性，因是向 AspectJ 取經的關係。
        /// 但應用原則上： 1 Advice(Aspect instance) ←→ 1 Pointcut ←→ 1 Attribute ←→ N 纏繞目標
        /// 等同 => 1 Aspect ←→ N 纏繞目標
        /// 利用 Attribute 纏繞目標即可，讓“纏繞”精簡化才符合 AOP 的初心。
        /// 

        [SelectMethods("HasCustomAttributeType:'SheepAspectLab.Aspects.CatchAndLogAttribute'")]
        public void CatchAndLogPointcut() { }

        [Around("CatchAndLogPointcut")]
        public object CatchAndLogAdvice(MethodJointPoint jp)
        {
            ///
            /// 寫 Log 機制在此為急用所以亂作，正式的應用應導入正式的 Log 模組，如：NLog 等。
            ///
            SheepAspectLab.Form1.WriteLog(string.Format("{1} >> ON ENTER : {0}", jp.Method.Name, "LogAdvice"));
            try
            {
                object result = jp.Execute();
                return result;
            }
            catch (Exception ex)
            {
                SheepAspectLab.Form1.WriteLog(string.Format("{1} >> ON EXCEPTION : {0}", ex.Message, "LogAdvice"));
                MessageBox.Show(ex.Message, "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return null;
                //throw;
            }
            finally
            {
                SheepAspectLab.Form1.WriteLog(string.Format("{1} >> ON LEAVE : {0}", jp.Method.Name, "LogAdvice"));
            }
        }

        //[SelectMethods("Name:('*_Click') & InType:'SheepAspectLab.Form1'")] // 不建議用此方法做 weaving。
        [SelectMethods("HasCustomAttributeType:'SheepAspectLab.Aspects.WaitCursorAttribute'")]
        public void WaitCursorPointcut() { }

        [Around("WaitCursorPointcut")]
        public object WaitCursorAdvice(MethodJointPoint jp)
        {
            SheepAspectLab.Form1.WriteLog(string.Format("{1} >> ON ENTER : {0}", jp.Method.Name, "WaitCursorAdvice"));
            System.Windows.Forms.Form frm = (Form)jp.This;
            try
            {
                frm.Cursor = Cursors.WaitCursor;

                object result = jp.Execute();
                return result;
            }
            catch (Exception ex)
            {
                SheepAspectLab.Form1.WriteLog(string.Format("{1} >> ON EXCEPTION : {0}", ex.Message, "WaitCursorAdvice"));
                throw;
            }
            finally
            {
                frm.Cursor = Cursors.Default;
                SheepAspectLab.Form1.WriteLog(string.Format("{1} >> ON LEAVE : {0}", jp.Method.Name, "WaitCursorAdvice"));
            }
        }
    }
}