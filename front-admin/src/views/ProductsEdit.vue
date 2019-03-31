<template>
  <div>
    <div
      class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom"
    >
      <h1 class="h2">Dodaj/edytuj produkt</h1>
    </div>
    <b-form @submit="onSubmit" v-if="show">
      <b-form-group id="input-group-1" label="Nazwa produktu:" label-for="input-1">
        <b-form-input id="input-1" v-model="form.name" required></b-form-input>
      </b-form-group>

      <b-form-group id="input-group-2" label="Cena:" label-for="input-2">
        <b-form-input id="input-2" type="number" v-model="form.price" required></b-form-input>
      </b-form-group>

      <b-form-group id="input-group-3" label="Kategoria:" label-for="input-3">
        <b-form-select id="input-3" v-model="form.categoryId" :options="categories" required></b-form-select>
      </b-form-group>

      <b-form-group id="input-group-3" label="Opis:" label-for="input-3">
        <b-form-textarea
          id="textarea"
          v-model="form.description"
          rows="3"
          max-rows="6"
        ></b-form-textarea>
      </b-form-group>

      <h2>Atrybuty</h2>
      <b-form-select id="input-3" v-model="attributeToAdd" :options="attributes" required></b-form-select>      
      <b-button @click="addAttribute()">Dodaj</b-button>
      <b-form-group id="input-group-3" label-for="input-3" v-for="attribute in form.attributes" v-bind:key="attribute.attributeId"
        v-bind:label="attributes.find(a => a.value == attribute.attributeId).text">
        <b-form-input id="input-1" v-model="attribute.attributeValue" required></b-form-input>
        <b-button @click="removeAttribute(attribute)">Usuń</b-button>
      </b-form-group>

      <b-button type="submit" variant="primary">Zapisz</b-button>
    </b-form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      attributeToAdd: null,
      form: {
        name: "",
        price: 0,
        description: "",
        categoryId: null,
        attributes: []
      },
      show: true
    };
  },
  methods: {
    onSubmit(evt) {
      evt.preventDefault();
      this.$store.dispatch("products/saveProductAction", this.form);
    },
    addAttribute() {
        this.form.attributes.push({ 
            attributeId: this.attributeToAdd,
            attributeValue: null
        })
    },
    removeAttribute(attribute) {
        this.form.attributes.splice(this.form.attributes.indexOf(attribute), 1);
    }
  },
  computed: {
    categories() {
        // todo category tree
      return this.$store.state.products.categoriesList.map(function(cat) {
        return { text: cat.name, value: cat.id };
      });
    },
    attributes() {
      return this.$store.state.products.attributesList.map(function(attr) {
        return { text: attr.name, value: attr.id };
      });
    }
  },
  created() {
    this.$store.dispatch("products/getCategoriesAction");
    this.$store.dispatch("products/getAttributesAction");    
  }
};
</script>

<style>
</style>
