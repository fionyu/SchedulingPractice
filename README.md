# �Ƶ{�B�z���m��



# �ؼ�:

�Ҧ��Ƶ{���ɶ��w�Ƭ����A����b jobs �o table �� (schema �аѦҤU�軡��)�C�o�m�ߪ��D�n�ؼЬO�A�b jobs database �u�䴩�Q�ʬd�ߪ�����U�A
�г]�p worker �������ȡC

���Ȫ��ݨD (��������������):

1. �Ҧ� job �������b���w�ɶ��d�򤺳Q�Ұ� (�w���ɶ� + �i����������d��)
1. �C�� job ���u��Q����@��, ���঳�B�z��@�b���������p
1. �����䴩���������� (�h�� worker, ������t��, �ब�۳ƴ�, �䴩�ʺA scaling)

���_�B�z����~����� (�����u������):

1. ���������Ҧ��W�z���ȻݨD
1. Jobs �M��d�ߪ����ƶV�ֶV�n
1. Jobs ���հ��楢�� (�m����lock) �����ƶV�ֶV�n
1. Jobs ���𪺮ɶ��V�u�V�n (����: ��ڱҰʮɶ� - �w���Ұʮɶ�)
  * ���𥭧���, �V�p�V�n
  * ����зǮt, �V�p�V�n
1. �ӧO job ���A�d�ߪ����� (���w job id) �V�ֶV�n

�̲׵��� = Sum(���� x �v��):
1. ���榨��, �V�C�V�n:
```querylist x 100.0 + acquire_failure x 10.0 + queryjob x 1.0```
1. ����{��, �V�C�V�n:
```average + stdev```


# ���ҷǳ�

���楻�d�ҵ{���A�ݭn�B�~�ǳ� SQL database. �ڦۤv�ϥ� LocalDB, �i�H���`�B��C

1. �Х��إ� database: ```JobsDB```
2. �Х� sql script: [database.txt](database.txt) �إ� table (�u����Ӫ��: jobs / workerlogs)
3. �Y���S�O���w�A�{���X�w�]�|�ϥγo�s���r��: ```Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JobsDB;Integrated Security=True;Pooling=False```


# ���D�W�d

�S�����󭭨�, �A�u�n�Ѧ� project: ```SchedulingPractice.Core``` �M��, �åB���� ```JobsRepo``` ���O, �Q��k�g�X�����ݨD���{���Y�i�C

# ���Ҥ覡

���m���D���Ѩ�� project:

1. SchedulingPractice.Core, class library, ���ѥ��n����ܮw (�D�n�O JobsRepo)
1. SchedulingPRactice.PubWorker, console app, �إߴ��ո�ơA�P��ܲέp��T�� console app

�ХΤU�C�覡���էA���{���O�_�ŦX�n�D�C

## ���ҥi�a�� (HA)

1. ���ӻ����ǳ����� (�u�ݭn���@��)
1. �Ұ� SchedulingPRactice.PubWorker, �w�]�|���� 10 min, �C������|�M����Ʈw���e, �Э@�ߵ���
1. �ЦP�ɱҰ� 5 ���A���{��
1. ����Ұ� 1 min ����A���H�����U CTRL-C ���N���_�A���{��
1. ���� SchedulingPRactice.PubWorker ���槹���A��ܲέp���G�C�T�{ "test result" �ƭ�:
  1. ```Complete Job``` �O�_�� 100% ? 
  1. ```Delay Too Long``` �O�_�� 0 ?
  1. ```Fail Job``` �O�_�� 0 ?


## ���ҮĲv

1. ���ӻ����ǳ����� (�u�ݭn���@��)
1. �Ұ� SchedulingPRactice.PubWorker, �w�]�|���� 10 min, �C������|�M����Ʈw���e, �Э@�ߵ���
1. �ЦP�ɱҰ� 5 ���A���{��
1. ���� SchedulingPRactice.PubWorker ���槹���A��ܲέp���G�C���� cost / efficient score. ��̪����ƶV�C�V�n�C

## ��X�̨βզX

���ӤW�z "���ҮĲv" ���{�ǡA���ܦP�ɱҰʵ{�����M�� (�w�] 5), ��X score �ƭȳ̧C���պA, �s�P PR �ɤ@�_���W�C