<template>
  <form @submit.prevent="handleSave" class="space-y-4">
    <div>
      <label for="title" class="block text-sm font-medium text-gray-700">Title</label>
      <input
        id="title"
        v-model="title"
        type="text"
        class="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
        required
      />
    </div>
    <div>
      <label for="content" class="block text-sm font-medium text-gray-700">Content</label>
      <textarea
        id="content"
        v-model="content"
        rows="6"
        class="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
        required
      ></textarea>
    </div>
    <button
      type="submit"
      class="inline-flex justify-center py-2 px-4 border border-transparent shadow-sm text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
    >
      Save
    </button>
  </form>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useNotesStore } from '@/stores/notesStore'
import { getNoteByIdApi } from '@/composables/useNotes'

const route = useRoute()
const router = useRouter()
const notesStore = useNotesStore()

const title = ref('')
const content = ref('')
const isEdit = ref(false)
const noteId = ref<string | null>(null)

onMounted(async () => {
  const id = route.params.id as string
  if (id && route.path.includes('/edit')) {
    isEdit.value = true
    noteId.value = id
    // Fetch note for editing
    try {
      const note = await getNoteByIdApi(id)
      title.value = note.title
      content.value = note.content
    } catch (error) {
      console.error('Failed to fetch note for editing:', error)
    }
  }
})

async function handleSave() {
  try {
    if (isEdit.value && noteId.value) {
      await notesStore.updateNote(noteId.value, { title: title.value, content: content.value })
      router.push(`/note/${noteId.value}`)
    } else {
      await notesStore.createNote({ title: title.value, content: content.value })
      router.push('/')
    }
  } catch (error) {
    alert('Failed to save note')
  }
}
</script>