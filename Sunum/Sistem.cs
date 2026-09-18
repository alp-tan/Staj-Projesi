using BusinessLayer.Concrate;
using BusinessLayer.DTO;
using DataAccesLayer.EntityFramework;
using EntityLayer.conc;
using EntityLayer.concrate;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Sunum
{
    public partial class Sistem : Form
    {
        LessonManager lessonManager = new LessonManager(new EfLessonDal());
        TeacherManager teacherManager = new TeacherManager(new EfTeacherDal());
        StudentManager studentManager = new StudentManager(new EfStudentDal());
        Sut_LessonManager sutLessonManager = new Sut_LessonManager(new EfSut_LessonDal());
        ExamManager _examManager = new ExamManager(new EfExamDal());
        Student_Lesson_ExamManager _notManager = new Student_Lesson_ExamManager(new EfStudent_Lesson_ExamDal());

        public Sistem()
        {
            InitializeComponent();
        }

        private void Sistem_Load(object sender, EventArgs e)
        {
            DersListesiniYenile();
        }
        private void DersListesiniYenile()
        {
            DersTablosu.DataSource = lessonManager.GetLessonDetails();
            DersTablosu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void OgretmenListesiniYenile()
        {
            OgretmenTablosu.DataSource = teacherManager.GetTeacherDetails();
            OgretmenTablosu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void OgrenciListesiniYenile()
        {
            OgrenciTablosu.DataSource = studentManager.GetStudentDetails();
            OgrenciTablosu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void KayitListesiniYenile()
        {
            OgrenciDersTablosu.DataSource = sutLessonManager.GetListWithDetails();
            OgrenciDersTablosu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void SinavListesiniYenile()
        {
            SinavTablosu.DataSource = _examManager.GetExamDetails();
            SinavTablosu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (SinavTablosu.Columns["ExamId"] != null)
            {
                SinavTablosu.Columns["ExamId"].Visible = false;
            }
        }
        private void NotListesiniYenile()
        {
            ÖğrenciSınavTablou.DataSource = _notManager.GetStudent_Lesson_ExamDetails();
            if (ÖğrenciSınavTablou.Columns["KayitId"] != null) ÖğrenciSınavTablou.Columns["KayitId"].Visible = false;
            ÖğrenciSınavTablou.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void OgretmenSekmesiniYukle()
        {
            OgretmenListesiniYenile();
            cmbDersOgr.DataSource = lessonManager.TGetAll();
            cmbDersOgr.DisplayMember = "LessonName";
            cmbDersOgr.ValueMember = "LessonId";
        }
        private void OgrenciDersSekmesiniYukle()
        {
            KayitListesiniYenile();
            cmbOgrenciOgrLes.DataSource = studentManager.TGetAll();
            cmbOgrenciOgrLes.DisplayMember = "StudentName";
            cmbOgrenciOgrLes.ValueMember = "StudentId";

            cmbLessonOgrLes.DataSource = lessonManager.TGetAll();
            cmbLessonOgrLes.DisplayMember = "LessonName";
            cmbLessonOgrLes.ValueMember = "LessonId";
        }
        private void SinavSekmesiniYukle()
        {
            SinavListesiniYenile();
            cmbLessonExm.DataSource = lessonManager.TGetAll();
            cmbLessonExm.DisplayMember = "LessonName";
            cmbLessonExm.ValueMember = "LessonId";
            cmbExamExm.DataSource = Enum.GetValues(typeof(ExamType));
        }
        private void NotSekmesiniYukle()
        {
            NotListesiniYenile();
            cmbStudentStdExam.DataSource = studentManager.TGetAll();
            cmbStudentStdExam.DisplayMember = "StudentName";
            cmbStudentStdExam.ValueMember = "StudentId";
        }
        private void SonucSekmesiniYukle()
        {
            cmbDersSonuç.DataSource = lessonManager.TGetAll();
            cmbDersSonuç.DisplayMember = "LessonName";
            cmbDersSonuç.ValueMember = "LessonId";
        }
        private void tabSayfalar_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabSayfalar.SelectedIndex)
            {
                case 0: DersListesiniYenile(); break;
                case 1: OgretmenSekmesiniYukle(); break;
                case 2: OgrenciListesiniYenile(); break;
                case 3: OgrenciDersSekmesiniYukle(); break;
                case 4: SinavSekmesiniYukle(); break;
                case 5: NotSekmesiniYukle(); break;
                case 6: SonucSekmesiniYukle(); break;
            }
        }
        private void btnLessonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                bool dersVarMi = lessonManager.TGetAll().Any(x => x.LessonName.ToLower() == txtLesson.Text.ToLower());
                if (dersVarMi) { MessageBox.Show("Bu ders zaten mevcut!"); return; }

                Lesson lesson = new Lesson();
                lesson.LessonName = txtLesson.Text;
                lessonManager.TInsert(lesson);
                MessageBox.Show("Ders başarıyla eklendi.");
                DersListesiniYenile();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btnLessonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (DersTablosu.CurrentRow != null)
                {
                    int id = Convert.ToInt32(DersTablosu.CurrentRow.Cells["DersID"].Value);
                    lessonManager.TDelete(lessonManager.TGetById(id));
                    MessageBox.Show("Ders silindi.");
                    DersListesiniYenile();
                }
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }
        private void btnLessonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (DersTablosu.CurrentRow != null)
                {
                    int seciliSatirIndex = DersTablosu.CurrentRow.Index;
                    int id = Convert.ToInt32(DersTablosu.CurrentRow.Cells["DersID"].Value);
                    var guncellenecekDers = lessonManager.TGetById(id);
                    if (guncellenecekDers != null)
                    {
                        guncellenecekDers.LessonName = txtLesson.Text;
                        lessonManager.TUpdate(guncellenecekDers);
                        MessageBox.Show("Ders güncellendi.");
                        DersListesiniYenile();
                        if (seciliSatirIndex < DersTablosu.Rows.Count)
                        {
                            DersTablosu.ClearSelection();
                            DersTablosu.Rows[seciliSatirIndex].Selected = true;
                            DersTablosu.CurrentCell = DersTablosu.Rows[seciliSatirIndex].Cells[0];
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }
        private void btnTeacherAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtOgretmenYeni.Text))
                {
                    MessageBox.Show("Öğretmen adı boş olamaz!");
                    return;
                }
                if (cmbDersOgr.SelectedValue == null)
                {
                    MessageBox.Show("Lütfen bir ders seçin!");
                    return;
                }
                int secilenDersId = 0;
                if (!int.TryParse(cmbDersOgr.SelectedValue.ToString(), out secilenDersId) || secilenDersId == 0)
                {
                    MessageBox.Show("Ders seçimi okunamadı. Lütfen dersi tekrar seçin.");
                    return;
                }
                Teacher yeniOgretmen = new Teacher();
                yeniOgretmen.TeacherName = txtOgretmenYeni.Text;
                yeniOgretmen.LessonId = secilenDersId;
                teacherManager.TInsert(yeniOgretmen);
                MessageBox.Show("Öğretmen başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OgretmenSekmesiniYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Öğretmen eklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTeacherDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (OgretmenTablosu.CurrentRow != null)
                {
                    int id = Convert.ToInt32(OgretmenTablosu.CurrentRow.Cells["OgretmenID"].Value);
                    teacherManager.TDelete(teacherManager.TGetById(id));
                    MessageBox.Show("Öğretmen silindi.");
                    OgretmenListesiniYenile();
                }
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        private void btnTeacherUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (OgretmenTablosu.CurrentRow != null)
                {
                    int seciliSatirIndex = OgretmenTablosu.CurrentRow.Index;
                    int id = Convert.ToInt32(OgretmenTablosu.CurrentRow.Cells["OgretmenID"].Value);
                    var ogretmen = teacherManager.TGetById(id);
                    if (ogretmen != null)
                    {
                        ogretmen.TeacherName = txtOgretmenYeni.Text;
                        teacherManager.TUpdate(ogretmen);
                        MessageBox.Show("Öğretmen güncellendi.");
                        OgretmenListesiniYenile();
                        if (seciliSatirIndex < OgretmenTablosu.Rows.Count)
                        {
                            OgretmenTablosu.ClearSelection();
                            OgretmenTablosu.Rows[seciliSatirIndex].Selected = true;
                            OgretmenTablosu.CurrentCell = OgretmenTablosu.Rows[seciliSatirIndex].Cells[0];
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }
        private void btnStudentAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Student student = new Student();
                student.StudentName = txtStudent.Text;
                studentManager.TInsert(student);
                MessageBox.Show("Öğrenci eklendi.");
                OgrenciListesiniYenile();
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }
        private void BtnStudentDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (OgrenciTablosu.CurrentRow != null)
                {
                    int id = Convert.ToInt32(OgrenciTablosu.CurrentRow.Cells["OgrenciID"].Value);
                    studentManager.TDelete(studentManager.TGetById(id));
                    MessageBox.Show("Öğrenci silindi.");
                    OgrenciListesiniYenile();
                }
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        private void btnStudentUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (OgrenciTablosu.CurrentRow != null)
                {
                    int seciliSatirIndex = OgrenciTablosu.CurrentRow.Index;
                    int id = Convert.ToInt32(OgrenciTablosu.CurrentRow.Cells["OgrenciID"].Value);
                    var ogrenci = studentManager.TGetById(id);
                    if (ogrenci != null)
                    {
                        ogrenci.StudentName = txtStudent.Text;
                        studentManager.TUpdate(ogrenci);
                        MessageBox.Show("Öğrenci güncellendi.");
                        OgrenciListesiniYenile();
                        if (seciliSatirIndex < OgrenciTablosu.Rows.Count)
                        {
                            OgrenciTablosu.ClearSelection();
                            OgrenciTablosu.Rows[seciliSatirIndex].Selected = true;
                            OgrenciTablosu.CurrentCell = OgrenciTablosu.Rows[seciliSatirIndex].Cells[0];
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        private void btnStudentLessonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbOgrenciOgrLes.SelectedValue != null && cmbLessonOgrLes.SelectedValue != null)
                {
                    int ogrenciId = Convert.ToInt32(cmbOgrenciOgrLes.SelectedValue);
                    int dersId = Convert.ToInt32(cmbLessonOgrLes.SelectedValue);

                    if (sutLessonManager.TGetAll().Any(x => x.StudentId == ogrenciId && x.LessonId == dersId))
                    {
                        MessageBox.Show("Bu öğrenci zaten bu derse kayıtlı!");
                        return;
                    }

                    Sut_Lesson yeniKayit = new Sut_Lesson();
                    yeniKayit.StudentId = ogrenciId;
                    yeniKayit.LessonId = dersId;
                    sutLessonManager.TInsert(yeniKayit);
                    MessageBox.Show("Kayıt başarılı.");
                    KayitListesiniYenile();
                }
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        private void btnStudentLessonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (OgrenciDersTablosu.CurrentRow != null)
                {
                    int id = Convert.ToInt32(OgrenciDersTablosu.CurrentRow.Cells["KayitNo"].Value);
                    sutLessonManager.TDelete(sutLessonManager.TGetById(id));
                    MessageBox.Show("Ders kaydı silindi.");
                    KayitListesiniYenile();
                }
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }
        private void DersTablosu_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtLessonID.Text = DersTablosu.Rows[e.RowIndex].Cells["DersID"].Value?.ToString();
            }
        }

        private void OgretmenTablosu_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtOgretmenID.Text = OgretmenTablosu.Rows[e.RowIndex].Cells["OgretmenID"].Value?.ToString();
                txtOgretmenYeni.Text = OgretmenTablosu.Rows[e.RowIndex].Cells["OgretmenAdi"].Value?.ToString();
            }
        }

        private void OgrenciTablosu_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtStudentID.Text = OgrenciTablosu.Rows[e.RowIndex].Cells["OgrenciID"].Value?.ToString();
                txtStudent.Text = OgrenciTablosu.Rows[e.RowIndex].Cells["OgrenciAdi"].Value?.ToString();
            }
        }
        private void btnExamAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbLessonExm.SelectedValue != null && cmbExamExm.SelectedValue != null && !string.IsNullOrWhiteSpace(txtCredit.Text))
                {
                    int dersId = Convert.ToInt32(cmbLessonExm.SelectedValue);
                    ExamType sinavTuru = (ExamType)cmbExamExm.SelectedValue;
                    int agirlik = Convert.ToInt32(txtCredit.Text);
                    if (_examManager.TGetAll().Any(x => x.LessonId == dersId && x.Type == sinavTuru))
                    {
                        MessageBox.Show("Bu derse ait bu sınav türü zaten tanımlanmış!");
                        return;
                    }
                    Exam yeniSinav = new Exam();
                    yeniSinav.LessonId = dersId;
                    yeniSinav.Type = sinavTuru;
                    yeniSinav.Ağırlık = agirlik;
                    _examManager.TInsert(yeniSinav);

                    MessageBox.Show("Sınav kuralı başarıyla eklendi.");
                    int secilenDersId = Convert.ToInt32(cmbLessonExm.SelectedValue);
                    SinavTablosu.DataSource = _examManager.GetExamDetailsByLesson(secilenDersId);
                }
                else
                {
                    MessageBox.Show("Lütfen ders, sınav türü ve ağırlık bilgilerini eksiksiz girin.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Kural Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SinavTablosu_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = SinavTablosu.Rows[e.RowIndex];
                cmbLessonExm.Text = row.Cells["DersAdi"].Value?.ToString();
                cmbExamExm.Text = row.Cells["SinavTuru"].Value?.ToString();
                txtCredit.Text = row.Cells["Agirlik"].Value?.ToString();
            }
        }

        private void btnExamDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (SinavTablosu.CurrentRow != null)
                {
                    int id = Convert.ToInt32(SinavTablosu.CurrentRow.Cells["ExamId"].Value);
                    var silinecekSinav = _examManager.TGetById(id);
                    if (silinecekSinav != null)
                    {
                        _examManager.TDelete(silinecekSinav);
                        MessageBox.Show("Sınav kuralı başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        int secilenDersId = Convert.ToInt32(cmbLessonExm.SelectedValue);
                        SinavTablosu.DataSource = _examManager.GetExamDetailsByLesson(secilenDersId);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen silmek istediğiniz sınavı tablodan seçin.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme işlemi sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExamUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (SinavTablosu.CurrentRow != null)
                {
                    if (cmbLessonExm.SelectedValue != null && cmbExamExm.SelectedValue != null && !string.IsNullOrWhiteSpace(txtCredit.Text))
                    {
                        int seciliSatirIndex = SinavTablosu.CurrentRow.Index;
                        int id = Convert.ToInt32(SinavTablosu.CurrentRow.Cells["ExamId"].Value);

                        var guncellenecekSinav = _examManager.TGetById(id);
                        if (guncellenecekSinav != null)
                        {
                            guncellenecekSinav.LessonId = Convert.ToInt32(cmbLessonExm.SelectedValue);
                            guncellenecekSinav.Type = (ExamType)cmbExamExm.SelectedValue;
                            guncellenecekSinav.Ağırlık = Convert.ToInt32(txtCredit.Text);

                            _examManager.TUpdate(guncellenecekSinav);
                            MessageBox.Show("Sınav yüzdesi başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            int secilenDersId = Convert.ToInt32(cmbLessonExm.SelectedValue);
                            SinavTablosu.DataSource = _examManager.GetExamDetailsByLesson(secilenDersId);
                            if (seciliSatirIndex < SinavTablosu.Rows.Count)
                            {
                                SinavTablosu.ClearSelection();
                                SinavTablosu.Rows[seciliSatirIndex].Selected = true;
                                SinavTablosu.CurrentCell = SinavTablosu.Rows[seciliSatirIndex].Cells["DersAdi"];
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lütfen ders, sınav türü ve ağırlık bilgilerini eksiksiz girin.");
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen güncellemek istediğiniz sınavı tablodan seçin.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Kural Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void cmbLessonExm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbLessonExm.SelectedValue != null && int.TryParse(cmbLessonExm.SelectedValue.ToString(), out int secilenDersId))
                {
                    SinavTablosu.DataSource = _examManager.GetExamDetailsByLesson(secilenDersId);
                    if (SinavTablosu.Columns["ExamId"] != null)
                    {
                        SinavTablosu.Columns["ExamId"].Visible = false;
                    }
                    SinavTablosu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ders filtrelenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnStudentExamAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbStudentStdExam.SelectedValue != null &&
                    cmbLessonStdExm.SelectedValue != null &&
                    cmbExamStdExm.SelectedValue != null &&
                    !string.IsNullOrWhiteSpace(txtExamGrade.Text))
                {
                    Student_Lesson_Exam yeniNot = new Student_Lesson_Exam();
                    yeniNot.Sut_LessonId = Convert.ToInt32(cmbLessonStdExm.SelectedValue);
                    yeniNot.ExamId = Convert.ToInt32(cmbExamStdExm.SelectedValue);
                    yeniNot.Grade = Convert.ToInt32(txtExamGrade.Text);

                    _notManager.TInsert(yeniNot);

                    MessageBox.Show("Öğrenci notu başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string secilenOgrenciAdi = cmbStudentStdExam.Text;
                    ÖğrenciSınavTablou.DataSource = _notManager.GetStudent_Lesson_ExamDetails()
                                                               .Where(x => x.OgrenciAdi == secilenOgrenciAdi).ToList();
                }
                else
                {
                    MessageBox.Show("Lütfen öğrenciyi, dersi, sınavı ve notu eksiksiz girin.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Kural Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnStudentExamDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ÖğrenciSınavTablou.CurrentRow != null)
                {
                    int id = Convert.ToInt32(ÖğrenciSınavTablou.CurrentRow.Cells["KayitId"].Value);
                    _notManager.TDelete(_notManager.TGetById(id));

                    MessageBox.Show("Not başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string secilenOgrenciAdi = cmbStudentStdExam.Text;
                    ÖğrenciSınavTablou.DataSource = _notManager.GetStudent_Lesson_ExamDetails()
                                                               .Where(x => x.OgrenciAdi == secilenOgrenciAdi).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStudentExamUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ÖğrenciSınavTablou.CurrentRow != null && !string.IsNullOrWhiteSpace(txtExamGrade.Text))
                {
                    int seciliSatirIndex = ÖğrenciSınavTablou.CurrentRow.Index;
                    int id = Convert.ToInt32(ÖğrenciSınavTablou.CurrentRow.Cells["KayitId"].Value);

                    var guncellenecekNot = _notManager.TGetById(id);
                    if (guncellenecekNot != null)
                    {
                        guncellenecekNot.Grade = Convert.ToInt32(txtExamGrade.Text);

                        _notManager.TUpdate(guncellenecekNot);
                        MessageBox.Show("Not başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string secilenOgrenciAdi = cmbStudentStdExam.Text;
                        ÖğrenciSınavTablou.DataSource = _notManager.GetStudent_Lesson_ExamDetails()
                                                                   .Where(x => x.OgrenciAdi == secilenOgrenciAdi).ToList();

                        if (seciliSatirIndex < ÖğrenciSınavTablou.Rows.Count)
                        {
                            ÖğrenciSınavTablou.ClearSelection();
                            ÖğrenciSınavTablou.Rows[seciliSatirIndex].Selected = true;
                            ÖğrenciSınavTablou.CurrentCell = ÖğrenciSınavTablou.Rows[seciliSatirIndex].Cells["OgrenciAdi"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbStudentStdExam_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (cmbStudentStdExam.SelectedValue != null && int.TryParse(cmbStudentStdExam.SelectedValue.ToString(), out int secilenOgrenciId))
                {
                    var ogrenciDersleri = sutLessonManager.GetListWithDetails()
                        .Where(x => x.OgrenciId == secilenOgrenciId).ToList();

                    cmbLessonStdExm.DataSource = ogrenciDersleri;
                    cmbLessonStdExm.DisplayMember = "DersAdi";
                    cmbLessonStdExm.ValueMember = "KayitNo";
                    string secilenOgrenciAdi = cmbStudentStdExam.Text;
                    var ogrenciNotlari = _notManager.GetStudent_Lesson_ExamDetails()
                                                    .Where(x => x.OgrenciAdi == secilenOgrenciAdi).ToList();

                    ÖğrenciSınavTablou.DataSource = ogrenciNotlari;
                }
            }
            catch { }
        }

        private void cmbLessonStdExm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbLessonStdExm.SelectedItem != null && cmbLessonStdExm.SelectedItem is SutLessonDto secilenDers)
                {
                    int gercekDersId = secilenDers.DersId;
                    var sinavlar = _examManager.TGetAll().Where(x => x.LessonId == gercekDersId).ToList();

                    cmbExamStdExm.DataSource = sinavlar;
                    cmbExamStdExm.DisplayMember = "Type";
                    cmbExamStdExm.ValueMember = "ExamId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filtreleme hatası: " + ex.Message);
            }
        }

        private void ÖğrenciSınavTablou_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = ÖğrenciSınavTablou.Rows[e.RowIndex];
                txtExamGrade.Text = row.Cells["Notu"].Value?.ToString();
            }
        }
        private void cmbDersSonuç_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbDersSonuç.SelectedValue != null && int.TryParse(cmbDersSonuç.SelectedValue.ToString(), out int secilenDersId))
                {
                    var rapor = _notManager.GetDersSonucu(secilenDersId);
                    SonuçTablosu.DataSource = rapor.OgrenciListesi;
                    if (SonuçTablosu.Columns["OgrenciId"] != null) SonuçTablosu.Columns["OgrenciId"].Visible = false;
                    SonuçTablosu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    txtGeçmeNotu.Text = rapor.GecmeNotu.ToString();
                    txtGeçen.Text = rapor.GecenKisi.ToString();
                    txtKalan.Text = rapor.KalanKisi.ToString();
                }
            }
            catch
            {
            }
        }

        private void txtLesson_TextChanged(object sender, EventArgs e) { }
        private void DersTablosu_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void tabSayfalar_MouseDoubleClick(object sender, MouseEventArgs e) { }
        private void tabLesson_MouseDoubleClick(object sender, MouseEventArgs e) { }
        private void cmbOgretmenDers_SelectedIndexChanged(object sender, EventArgs e) { }
        private void tabStudent_MouseClick(object sender, MouseEventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void pageStudentLesson_Click(object sender, EventArgs e) { }
        private void StudenLessonDelete_SelectedIndexChanged(object sender, EventArgs e) { }
        private void OgrenciTablosu_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void tabStudent_Click(object sender, EventArgs e) { }
        private void SinavTablosu_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}