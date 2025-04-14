## Questão 4:

> [!NOTE]  
> O comando abaixo foi feito e testado no SQL SERVER 2019 usando o SQL Server Management Studio 20.

```sql
SELECT ASSUNTO, ANO, COUNT(*) AS TOTAL
FROM ATENDIMENTOS
GROUP BY ASSUNTO, ANO
HAVING COUNT(*) > 3
ORDER BY ANO DESC, TOTAL DESC;
```

## Ilustração:

![Questao4](https://github.com/matheusfladislau/TesteTecnico_Ailos/blob/main/questao_4/img/questao4_resultado_esperado.png)
