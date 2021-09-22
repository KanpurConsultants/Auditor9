<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmCustomerPaymentFollowup


    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.PnlBillWiseDetail = New System.Windows.Forms.Panel()
        Me.BtnOk = New System.Windows.Forms.Button()
        Me.PNL1 = New System.Windows.Forms.Panel()
        Me.TC1 = New System.Windows.Forms.TabControl()
        Me.TpMonthWiseDetail = New System.Windows.Forms.TabPage()
        Me.TpBillWiseDetail = New System.Windows.Forms.TabPage()
        Me.PnlMonthWiseDetail = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TC1.SuspendLayout()
        Me.TpMonthWiseDetail.SuspendLayout()
        Me.TpBillWiseDetail.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.BtnOk, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.PNL1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TC1, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 185.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(634, 566)
        Me.TableLayoutPanel1.TabIndex = 745
        '
        'PnlBillWiseDetail
        '
        Me.PnlBillWiseDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlBillWiseDetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PnlBillWiseDetail.Location = New System.Drawing.Point(3, 3)
        Me.PnlBillWiseDetail.Name = "PnlBillWiseDetail"
        Me.PnlBillWiseDetail.Size = New System.Drawing.Size(614, 144)
        Me.PnlBillWiseDetail.TabIndex = 748
        '
        'BtnOk
        '
        Me.BtnOk.BackColor = System.Drawing.Color.Transparent
        Me.BtnOk.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOk.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOk.Location = New System.Drawing.Point(543, 536)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(88, 27)
        Me.BtnOk.TabIndex = 745
        Me.BtnOk.Text = "OK"
        Me.BtnOk.UseVisualStyleBackColor = False
        '
        'PNL1
        '
        Me.PNL1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PNL1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PNL1.Location = New System.Drawing.Point(3, 3)
        Me.PNL1.Name = "PNL1"
        Me.PNL1.Size = New System.Drawing.Size(628, 342)
        Me.PNL1.TabIndex = 746
        '
        'TC1
        '
        Me.TC1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TC1.Controls.Add(Me.TpMonthWiseDetail)
        Me.TC1.Controls.Add(Me.TpBillWiseDetail)
        Me.TC1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TC1.Location = New System.Drawing.Point(3, 351)
        Me.TC1.Name = "TC1"
        Me.TC1.SelectedIndex = 0
        Me.TC1.Size = New System.Drawing.Size(628, 179)
        Me.TC1.TabIndex = 0
        '
        'TpMonthWiseDetail
        '
        Me.TpMonthWiseDetail.Controls.Add(Me.PnlMonthWiseDetail)
        Me.TpMonthWiseDetail.Location = New System.Drawing.Point(4, 25)
        Me.TpMonthWiseDetail.Name = "TpMonthWiseDetail"
        Me.TpMonthWiseDetail.Padding = New System.Windows.Forms.Padding(3)
        Me.TpMonthWiseDetail.Size = New System.Drawing.Size(620, 150)
        Me.TpMonthWiseDetail.TabIndex = 0
        Me.TpMonthWiseDetail.Text = "Month Wise Detail"
        Me.TpMonthWiseDetail.UseVisualStyleBackColor = True
        '
        'TpBillWiseDetail
        '
        Me.TpBillWiseDetail.Controls.Add(Me.PnlBillWiseDetail)
        Me.TpBillWiseDetail.Location = New System.Drawing.Point(4, 25)
        Me.TpBillWiseDetail.Name = "TpBillWiseDetail"
        Me.TpBillWiseDetail.Padding = New System.Windows.Forms.Padding(3)
        Me.TpBillWiseDetail.Size = New System.Drawing.Size(620, 150)
        Me.TpBillWiseDetail.TabIndex = 1
        Me.TpBillWiseDetail.Text = "Bill Wise Detail"
        Me.TpBillWiseDetail.UseVisualStyleBackColor = True
        '
        'PnlMonthWiseDetail
        '
        Me.PnlMonthWiseDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlMonthWiseDetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PnlMonthWiseDetail.Location = New System.Drawing.Point(3, 3)
        Me.PnlMonthWiseDetail.Name = "PnlMonthWiseDetail"
        Me.PnlMonthWiseDetail.Size = New System.Drawing.Size(614, 144)
        Me.PnlMonthWiseDetail.TabIndex = 749
        '
        'FrmCustomerPaymentFollowup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(634, 566)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.Name = "FrmCustomerPaymentFollowup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sales Dimensions"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TC1.ResumeLayout(False)
        Me.TpMonthWiseDetail.ResumeLayout(False)
        Me.TpBillWiseDetail.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents BtnOk As Button
    Friend WithEvents PnlBillWiseDetail As Panel
    Friend WithEvents PNL1 As Panel
    Friend WithEvents TC1 As TabControl
    Friend WithEvents TpMonthWiseDetail As TabPage
    Friend WithEvents TpBillWiseDetail As TabPage
    Friend WithEvents PnlMonthWiseDetail As Panel
End Class
