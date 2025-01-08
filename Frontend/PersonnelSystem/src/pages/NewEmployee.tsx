import { useEffect, useState } from "react"
import Subdivision from "../types/subdivision";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import './Forms.css';


const NewEmployeePage: React.FC = () => {
    const [subdivisionList, setSubdivisionList] = useState<Subdivision[]>([]);
    const [choosedId, setChoosedId] = useState(0);
    const [error, setError] = useState<string | null>(null);
    const [choosedName, setChoosedName] = useState<string>("");
    const navigate = useNavigate();
    const [loading, setLoading] = useState<boolean>(false);

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
                if (error.response.status === 401) {
                    navigate('/auth');
                }
                else if (error.response.status === 400) {
                    setError(error.response.data);
                }
            });           
        }

        preLoad();
    }, []);

    const handleChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setChoosedId(Number(event.target.value));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError(null);
        setLoading(true);
        console.log("name: " + choosedName);
        await axios.post("http://localhost:5000/api/react/employee/add", 
            {
                Name: choosedName,
                SubdivisionId: choosedId,
            },
            {
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': localStorage.getItem("AuthPass"),
                }
            }
        ).then(response => {
            if (response.status === 200) {
                alert("Успешно");
                setTimeout(() => {
                    window.location.reload();
                }, 2000);
            }
        }).catch(error => {
            setLoading(false);
            if (error.response) {
                if (error.response.status === 401) {
                    navigate('/auth');
                }
                else if (error.response.status === 400) {
                    setError(error.response.data);
                }
            }
        });
    }

    return (
        <div>
            <form onSubmit={handleSubmit} className="form-container">
                <label htmlFor="name">Введите имя:</label>
                <input type="text" id="name" onChange={(e) => setChoosedName(e.target.value)}/>
                <label htmlFor="subdivision">Выберите подразделение:</label>
                <select
                    id="subdivision"
                    value={choosedId}
                    onChange={handleChange}
                >
                    <option value="" disabled>Выберите подразделение:</option>
                    {subdivisionList.map((subdivision) => (
                        <option key={subdivision.id} value={subdivision.id}>
                            {subdivision.name}
                        </option>
                    ))}
                </select>
                <button type="submit" disabled={loading}>{loading ? "Загрузка" : "Создать"}</button>
            </form>
            {error && <span className="error">{error}</span>}
        </div>
    );
}

export default NewEmployeePage;