import { useEffect, useState } from "react"
import Subdivision from "../types/subdivision"
import { useNavigate } from "react-router-dom";
import axios from "axios";
import Employee from "../types/employee";
import './Forms.css';


const Page: React.FC = () => {
    const [subdivisionList, setSubdivisionList] = useState<Subdivision[]>([]);
    
    const [startDate, setStartDate] = useState<string>('');
    const [endDate, setEndDate] = useState<string>('');
    const [choosedId, setChoosedId] = useState<number>(0);

    const [loading, setLoading] = useState<boolean>(false);
    const [result, setResult] = useState<string | null>(null);

    const navigate = useNavigate();

    useEffect(() => {
        const preLoad = async () => {
            await axios.get('http://localhost:5000/api/react/subdivision/all', { 
            headers: {
                'Content-Type': 'application/json',
                'Authorization': localStorage.getItem("AuthPass"),
            }
            }).then(response => {
                if (response.status == 200) {
                    let list: Subdivision[] = [];
                    const subdivisions = response.data;
                    subdivisions.forEach((subdiv: Subdivision) => {
                        list.push(subdiv);
                    }); 
                    setSubdivisionList(list);
                }
            }).catch(error => {
                if (error.response.status == 401) {
                    navigate('/auth');
                }
            });
        }

        preLoad();
    }, []);

    const handleStartChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const selectedDate = e.target.value;
        if (selectedDate) {
          const dateObj = new Date(selectedDate);

          const isoString = dateObj.toISOString().split('T')[0] + 'T00:00:00.000Z';
    
          setStartDate(isoString);
        }
    };

    const handleEndChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const selectedDate = e.target.value;
        if (selectedDate) {
          const dateObj = new Date(selectedDate);

          const isoString = dateObj.toISOString().split('T')[0] + 'T00:00:00.000Z';
    
          setEndDate(isoString);
        }
    };

    const handleSubdivisionChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setChoosedId(Number(event.target.value));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setLoading(true);
        await axios.get("http://localhost:5000/api/react/employee/bydate", {
            params: {
                subdivisionId: choosedId,
                start: startDate,
                end: endDate,
            },
            headers: {
                'Content-Type': 'application/json',
                'Authorization': localStorage.getItem("AuthPass"),
            }
        }).then(response => {
            if (response.status === 200) {
                let rawResult = "";
                const employees: Employee[] = response.data;
                employees.forEach(employee => {
                    rawResult += employee.name + "; ";
                });
                setResult(rawResult);
            }
            setLoading(false);
        }).catch(error => {
            if (error.response) {
                if (error.response.status === 401) {
                    navigate('/auth');
                }
            }
        setLoading(false);
        });
    }

    return (
        <div>
            <h1>Поиск подразделений по дате</h1>
            <form onSubmit={handleSubmit} className="form-container">
                <label>
                Введите начальную дату:
                <input
                    type="date"
                    onChange={handleStartChange}
                />
                </label>
                <label>
                Введите конечную дату:
                <input
                    type="date"
                    onChange={handleEndChange}
                />
                </label>
                <label htmlFor="subdivision">Выберите подразделение:</label>
                <select
                    id="subdivision"
                    value={choosedId}
                    onChange={handleSubdivisionChange}
                >
                    {subdivisionList.map((subdivision) => (
                        <option key={subdivision.id} value={subdivision.id}>
                            {subdivision.name}
                        </option>
                    ))}
                </select>
                <button type="submit" disabled={loading}>
                {loading ? 'Загрузка...' : 'Найти'}
                </button>
            </form>

            {result && (
                <div
                style={{
                    marginTop: '20px',
                    padding: '10px',
                    border: '1px solid #ccc',
                    borderRadius: '5px',
                }}
                >
                <strong>Результат поиска:</strong> {result}
                </div>
            )}
            </div>
    );
}

export default Page;