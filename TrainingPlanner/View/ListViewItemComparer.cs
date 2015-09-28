using System;
using System.Collections;
using System.Windows.Forms;

namespace TrainingPlanner.View
{
  public class ListViewItemComparer : IComparer
  {
    private readonly int _col;
    private readonly bool _reverse;

    public ListViewItemComparer(int column, bool reverse)
    {
      this._col = column;
      this._reverse = reverse;
    }

    public int Column { get { return _col; } }
    public bool Reverse { get { return _reverse; } }

    public int Compare(object x, object y)
    {
      var sx = ((ListViewItem)x).SubItems[_col].Text;
      var sy = ((ListViewItem)y).SubItems[_col].Text;

      double dx, dy;
      TimeSpan tx, ty;

      // attempt double comparison
      if (double.TryParse(sx, out dx) && double.TryParse(sy, out dy))
      {
        if (dx == dy)
        {
          return 0;
        }
        return (_reverse ? -1 : 1) * (dx > dy ? 1 : -1);
      }

      // attempt timespan comparison
      if (TimeSpan.TryParse(sx, out tx) && TimeSpan.TryParse(sy, out ty))
      {
        if (tx == ty)
        {
          return 0;
        }
        return (_reverse ? -1 : 1) * (tx > ty ? 1 : -1);
      }

      // fall back to string comparison
      return (_reverse ? -1 : 1) * string.CompareOrdinal(((ListViewItem)x).SubItems[_col].Text, ((ListViewItem)y).SubItems[_col].Text);
    }
  }
}
