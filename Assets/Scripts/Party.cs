using System.Collections;
using System.Collections.Generic;

public class Party : IEnumerable<CharacterData>
{
	List<CharacterData> members;
	public int Count { get => members.Count; }
	public Party()
	{
		members = new List<CharacterData>(capacity: 4);
		for (int i = 0; i < 4; i++)
		{
			members.Add(null);
        }
	}

	public bool IsEmpty()
	{
		foreach (CharacterData member in members)
		{
			if (member != null)
			{
				return false;
			}
		}
		return true;
    }

    public bool IsAllDead()
	{
		foreach (CharacterData member in members)
		{
			if (member != null && !member.IsDead())
			{
				return false;
			}
		}
		return true;
    }

	public IReadOnlyCollection<CharacterData> GetMembers() {
		return members.AsReadOnly();
    }

	public CharacterData GetMember(int index)
	{
		return members[index];
    }
    public void SetMembers(List<CharacterData> newMembers) {
		members = new List<CharacterData>(newMembers);
	}

	public void SetMember(int index, CharacterData member) {
		members[index] = member;
    }

    public IEnumerator<CharacterData> GetEnumerator()
    {
        return members.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
