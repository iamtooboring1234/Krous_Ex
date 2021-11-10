CREATE OR REPLACE FUNCTION countRentalID (vCustomerID IN VARCHAR2) 
RETURN NUMBER IS
CountRentalID NUMBER := 0;

BEGIN
	SELECT COUNT(RentalID) 
	INTO countRentalID 
	FROM Rental
	WHERE CustomerID = vCustomerID;
	RETURN countRentalID;

END;
/


/*
select countRentalID('C00255') from dual;
*/