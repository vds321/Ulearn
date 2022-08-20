using System;
using System.Collections.Generic;
using System.Linq;

namespace Generics.Tables
{
    public class Table<TRow, TCol, TItem>
    {
        public readonly HashSet<TRow> Rows;
        public readonly HashSet<TCol> Columns;
        private readonly Dictionary<TRow, Dictionary<TCol, TItem>> _TableDictionary;

        public Table()
        {
            Rows = new HashSet<TRow>();
            Columns = new HashSet<TCol>();
            _TableDictionary = new Dictionary<TRow, Dictionary<TCol, TItem>>();
            Open = new OpenIndex(this);
            Existed = new ExistIndex(this);
        }

        public TItem this[TRow row, TCol col]
        {
            get => _TableDictionary[row][col];
            set
            {
                if (value.GetType() != typeof(TItem)) throw new ArgumentException();
                AddRow(row);
                AddColumn(col);
                _TableDictionary[row][col] = value;
            }
        }

        private bool IsRowExist(TRow row) => Rows.Count > 0 && Rows.Contains(row);

        private bool IsColumnExist(TCol column) => Columns.Count > 0 && Columns.Contains(column);

        public void AddRow(TRow row)
        {
            if (Rows.Add(row)) _TableDictionary.Add(row, null);
        }

        public void AddColumn(TCol column)
        {
            if (!Columns.Add(column)) return;
            var keys = _TableDictionary.Keys.ToList();
            foreach (var key in keys)
            {
                var dict = new Dictionary<TCol, TItem>()
                {
                    {column,default}
                };
                _TableDictionary[key] = dict;
            }
        }

        public OpenIndex Open { get; }
        public ExistIndex Existed { get; }
        public class ExistIndex
        {
            private readonly Table<TRow, TCol, TItem> _Table;

            public ExistIndex(Table<TRow, TCol, TItem> table) { _Table = table; }

            public TItem this[TRow row, TCol column]
            {
                get
                {
                    if (!_Table.IsRowExist(row) || !_Table.IsColumnExist(column)) throw new ArgumentException();
                    return _Table._TableDictionary[row][column] != null ? _Table._TableDictionary[row][column] : default;
                }
                set
                {
                    if (!_Table.IsRowExist(row) || !_Table.IsColumnExist(column) || value.GetType() != typeof(TItem)) throw new ArgumentException();
                    _Table._TableDictionary[row][column] = value;
                }
            }
        }

        public class OpenIndex
        {
            private readonly Table<TRow, TCol, TItem> _Table;
            public OpenIndex(Table<TRow, TCol, TItem> table) { _Table = table; }
            public TItem this[TRow row, TCol column]
            {
                get
                {
                    if (!_Table.IsRowExist(row) || !_Table.IsColumnExist(column) || _Table._TableDictionary[row][column] == null) return default;
                    return _Table._TableDictionary[row][column];
                }
                set
                {
                    if (value.GetType() != typeof(TItem)) throw new ArgumentException();
                    if (!_Table.IsRowExist(row)) _Table.AddRow(row);
                    if (!_Table.IsColumnExist(column)) _Table.AddColumn(column);
                    _Table._TableDictionary[row][column] = value;
                }
            }
        }
    }
}
